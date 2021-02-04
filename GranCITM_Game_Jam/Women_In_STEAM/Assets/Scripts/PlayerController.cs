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

    public float            m_speed             = 1.0f;
    public float            m_jump_force        = 1.0f;
    public float            m_jump_time         = 0.35f;

    public Transform        m_feet;
    public float            m_collision_radius  = 0.3f;
    public LayerMask        m_ground_layer;

    public LayerMask        m_hazards_layer;

    private Rigidbody2D     rb;
    private Collider2D      col;
    private float           jump_time_counter   = 0.0f;
    private float           direction           = 1.0f;

    // --- DEBUG ---
    private bool            debug_movement      = false;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        DebugControls();
    }

    void InitVariables()
    {
        rb  = GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * m_jump_force;
            player_state = PLAYER_STATE.JUMPING;
        }
    }

    void ExtendJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (jump_time_counter < m_jump_time)
            {
                rb.velocity = Vector2.up * m_jump_force;
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
        if (rb.velocity.y > 0.0f)
        {
            rb.gravityScale = 2.0f;
        }
        else
        {
            rb.gravityScale = 1.0f;
        }
        
        if (IsCollidingWithGround())
        {
            jump_time_counter = 0.0f;
            player_state = PLAYER_STATE.GROUNDED;
        }
    }

    void ResetGame()
    {
        // CHECKPOINTS?
    }

    bool IsCollidingWithGround()
    {
        return Physics2D.OverlapCircle(m_feet.position, m_collision_radius, m_ground_layer);
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
        rb.velocity = new Vector2(direction * m_speed, rb.velocity.y);
        debug_movement = false;
    }
}
