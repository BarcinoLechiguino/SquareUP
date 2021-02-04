using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PLAYER_STATE
    {
        GROUNDED,
        JUMPING,
        DEAD
    }

    [HideInInspector] public PLAYER_STATE player_state = PLAYER_STATE.GROUNDED;

    public float            m_speed             = 1.0f;
    public float            m_jump_force        = 1.0f;
    public float            m_jump_time         = 0.35f;

    public Transform        m_feet;
    public float            m_collision_radius  = 0.1f;
    public LayerMask        m_ground_layer;

    public LayerMask        m_hazards_layer;

    private Rigidbody2D     rb;
    private BoxCollider2D   col;
    private float           jump_input_value    = 0.0f;
    private float           jump_time_counter   = 0.0f;
    private float           direction           = 1.0f;
    private bool            is_grounded         = false;

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
        col = GetComponent<BoxCollider2D>();
    }

    void Jump()
    {
        jump_input_value = Input.GetAxis("Jump");

        switch (player_state)
        {
            case PLAYER_STATE.GROUNDED:                                                                         // JUMP FROM GROUND
                if (Input.GetKeyDown(KeyCode.Space)) 
                { 
                    rb.velocity     = Vector2.up * m_jump_force;
                    player_state    = PLAYER_STATE.JUMPING;
                }                   
            break;
            
            case PLAYER_STATE.JUMPING:
                if (Input.GetKey(KeyCode.Space))
                {
                    if (jump_time_counter < m_jump_time)
                    {
                        rb.velocity         = Vector2.up * m_jump_force;
                        jump_time_counter   += Time.deltaTime;
                    }
                }

                if (Physics2D.OverlapCircle(m_feet.position, m_collision_radius, m_ground_layer) || Input.GetKeyUp(KeyCode.Space))
                {
                    jump_time_counter   = 0.0f;
                    player_state        = PLAYER_STATE.GROUNDED;
                }
            break;
            
            case PLAYER_STATE.DEAD:
                // RESET GAME STATE
                // CHECKPOINTS??
            break;
        }
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
