using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PLAYER_STATE
    {
        GROUNDED,
        JUMPING,
        FALLING,
        DEAD
    }

    [HideInInspector] public PLAYER_STATE player_state = PLAYER_STATE.FALLING;
    public Animator         Anim;

    [HideInInspector] public int            figures_count       = 0;

    public float                            m_speed             = 1.0f;
    public float                            m_jump_force        = 1.0f;
    public float                            m_jump_time         = 0.35f;

    public Transform                        m_feet;
    public float                            m_collision_radius  = 0.3f;

    public GameObject                       m_jump_flag;
    [HideInInspector] public GameObject     current_jump_flag;
    
    public LayerMask                        m_ground_layer;
    public LayerMask                        m_hazards_layer;
    public LayerMask                        m_figures_layer;

    private TerrainGenerator                terrain_generator;
    public GameObject                       active_terrain;

    private Rigidbody2D                     rb;
    private Collider2D                      col;
    private Vector2                         jump_velocity           = Vector2.zero;
    public float                            jump_time_counter       = 0.0f;
    private bool                            normal_gravity          = true;
    private bool                            jump_released           = false;

    private Vector2                         original_position       = Vector2.zero;
    private Vector2                         current_position        = Vector2.zero;
    private bool                            needs_snapback          = false;
    private bool                            enact_snapback          = false;
    private float                           total_snapback_time     = 2.5f;
    private float                           current_snapback_time   = 0.0f;
    private float                           snapback_offset         = 2.0f;
    private float                           current_snapback_offset = 0.0f;

    private GameplayManager                 manager;

    // --- DEBUG ---
    private bool            debug_movement      = false;
    private float           direction           = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        SnapBack();

        //DebugControls();
        
        manager.SetFigureCount(figures_count);
    }

    void InitVariables()
    {
        rb  = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameplayManager>();

        original_position = transform.position;

        terrain_generator   = FindObjectOfType<TerrainGenerator>();
        active_terrain      = terrain_generator.active_terrains[0];
    }

    void Jump()
    {
        switch (player_state)
        {
            case PLAYER_STATE.GROUNDED:     { StartJump(); }    break;
            case PLAYER_STATE.JUMPING:      { ExtendJump(); }   break;
            case PLAYER_STATE.FALLING:      { EndJump(); }      break;
            case PLAYER_STATE.DEAD:         { ResetGame(); }    break;
        }
    }

    void StartJump()
    {
        jump_velocity = Vector2.up * m_jump_force * /*Time.deltaTime*/ 0.016f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = jump_velocity;
            Anim.SetBool("Jumping", true);
            player_state = PLAYER_STATE.JUMPING;

            GenerateJumpFlag();
        }
    }

    void GenerateJumpFlag()
    {
        current_jump_flag                       = Instantiate(m_jump_flag, m_feet.position, Quaternion.identity);
        
        active_terrain                          = terrain_generator.active_terrains[1];
        current_jump_flag.transform.parent      = active_terrain.transform;
    }

    void ExtendJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (jump_time_counter < m_jump_time)
            {
                rb.velocity = jump_velocity;
                jump_time_counter += Time.deltaTime;
            }
        }

        if ((jump_time_counter > m_jump_time) || Input.GetKeyUp(KeyCode.Space))
        {
            player_state = PLAYER_STATE.FALLING;
        }
    }

    void EndJump()
    {
        if (/*rb.velocity.y > 0.0f &&*/ normal_gravity)
        {
            rb.gravityScale += 1.0f;
            normal_gravity = false;
        }
        //else
        //{
        //    if (!normal_gravity)
        //    {
        //        rb.gravityScale -= 1.0f;
        //        normal_gravity = true;
        //    }
        //}
        
        if (IsCollidingWithGround())
        {
            if (!normal_gravity)
            {
                rb.gravityScale -= 1.0f;
                normal_gravity = true;
            }

            jump_time_counter = 0.0f;
            Anim.SetBool("Jumping", false);
            player_state = PLAYER_STATE.GROUNDED;

            //Debug.Log("JUMP ENDED");
        }
    }

    void SnapBack()
    {   
        if (transform.position.x != original_position.x)
        {
            needs_snapback = true;
            current_position = transform.position;
        }

        if (needs_snapback)
        {
            if (!enact_snapback)
            {
                current_snapback_offset += Time.deltaTime;

                if (current_snapback_offset > snapback_offset)
                {
                    current_snapback_offset = 0.0f;
                    enact_snapback = true;
                }
            }
            else
            {
                current_snapback_time += Time.deltaTime;

                float rate = (current_snapback_time / total_snapback_time);

                float lerped_x_pos = Mathf.Lerp(current_position.x, original_position.x, rate);

                transform.position = new Vector3(lerped_x_pos, transform.position.y, transform.position.z);

                if (current_snapback_time > total_snapback_time)
                {
                    transform.position = new Vector3(original_position.x, transform.position.y, transform.position.z);
                    current_snapback_time = 0.0f;
                    needs_snapback = false;
                    enact_snapback = false;
                }
            }
        }
    }

    void ResetGame()
    {
        manager.GameOver();
    }

    bool IsCollidingWithGround()
    {
        return Physics2D.OverlapCircle(m_feet.position, m_collision_radius, m_ground_layer);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Respawn"))
        {
            player_state = PLAYER_STATE.DEAD;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Mentor"))
        {
            manager.NextSector();
        }
    }

    public void AddFigure()
    {
        ++figures_count;
    }

    public void SubtractFigure()
    {
        --figures_count;
    }

    void DebugControls()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1.0f;
            debug_movement = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1.0f;
            debug_movement = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            debug_movement = false;
        }

        if (debug_movement)
        {
            Move();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(direction * m_speed * /*Time.deltaTime*/ 0.016f, rb.velocity.y);
        debug_movement = false;
    }
}
