using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    public enum FIGURE_STATE
    {
        IDLE,
        ACTIVE,
        DEAD
    }

    public enum JUMPING_STATE
    {
        GROUNDED,
        JUMPING,
        FALLING
    }

    [HideInInspector] public FIGURE_STATE   figure_state    = FIGURE_STATE.IDLE;
    [HideInInspector] public JUMPING_STATE  jumping_state   = JUMPING_STATE.GROUNDED;

    public LayerMask            m_ground_layer;
    public LayerMask            m_hazards_layer;
    public LayerMask            m_player_layer;
    public LayerMask            m_jump_flag_layer;
    public LayerMask            m_mentors_layer;

    public Transform            m_feet;
    public float                ground_collision_radius             = 0.1f;
    public float                player_collision_radius             = 0.5f;
    public float                mentor_collision_radius             = 0.4f;

    public float                m_jump_force                        = 350.0f;
    public float                m_jump_time                         = 0.4f;

    private Rigidbody2D         rb;
    private CapsuleCollider2D   col;
    private Vector2             jump_velocity                       = Vector2.zero;
    private float               jump_time_counter                   = 0.0f;
    private bool                jump_released                       = false;

    private bool                normal_gravity_scale                = true;

    private GameplayManager     manager;

    public Animator             Anim;
    // --- PLAYER VARIABLES ---
    private Transform           player_transform;
    private PlayerController    player_controller;

    // --- JUICE VARIABLES ---
    private float               position_behind_player              = 0.0f;
    private float               new_position_behind_player          = 0.0f;
    private float               original_position_behind_player     = 0.0f;
    private bool                calculated_new_pos                  = false;
    private float               total_lerp_time                     = 2.0f;
    private float               current_lerp_time                   = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFigureState();

        if (figure_state == FIGURE_STATE.ACTIVE)
        {
            UpdateJumpingState();
        }
    }

    void UpdateFigureState()
    {
        if (CheckForFigureDeath())
        {
            figure_state = FIGURE_STATE.DEAD;
            // DESTROY THIS INSTACE?
        }

        switch (figure_state)
        {
            case FIGURE_STATE.IDLE:     { CheckForPlayerPickUp(); Anim.SetBool("Static", true); } break;
            case FIGURE_STATE.ACTIVE:   { FollowPlayer(); Anim.SetBool("Static", false); }        break;
            case FIGURE_STATE.DEAD:     { DestroyFigure(); }                                      break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Respawn"))
        {
            player_controller.SubtractFigure();
            Destroy(gameObject);
        }
    }
    bool CheckForFigureDeath()
    {
        bool ret = false;



        return ret;
    }

    bool CheckForPlayerPickUp()
    {
        bool ret = false;

        if (Physics2D.OverlapCircle(transform.position, player_collision_radius, m_player_layer))
        {
            figure_state        = FIGURE_STATE.ACTIVE;

            transform.parent    = null;
            transform.position  = new Vector3(player_transform.position.x - position_behind_player, player_transform.position.y, player_transform.position.z);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            player_controller.AddFigure();

            if (gameObject.tag.Equals("Mentor"))
            {
                manager.NextSector();
            }
        }

        return ret;
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(player_transform.position.x - position_behind_player, transform.position.y, transform.position.z);

        if (!manager.hard_mode)
        {
            if (jumping_state != JUMPING_STATE.GROUNDED)
            {
                return;
            }
        }

        if (!calculated_new_pos)
        {
            new_position_behind_player = 1.0f + Random.Range(0.0f, 3.0f);
            original_position_behind_player = position_behind_player;

            calculated_new_pos = true;
        }
        else
        {
            current_lerp_time += Time.deltaTime;

            float rate = (current_lerp_time / total_lerp_time);

            position_behind_player = Mathf.Lerp(original_position_behind_player, new_position_behind_player, rate);

            if (current_lerp_time > total_lerp_time)
            {
                position_behind_player = new_position_behind_player;
                current_lerp_time = 0.0f;
                calculated_new_pos = false;
            }
        }
    }

    void DestroyFigure()
    {
        player_controller.SubtractFigure();
    }

    void UpdateJumpingState()
    {
       //Debug.Log(jumping_state);
        switch (jumping_state)
        {
            case JUMPING_STATE.GROUNDED:    { StartFigureJump(); Anim.SetBool("Jumping", false); Anim.SetBool("Falling", false); }      break;
            case JUMPING_STATE.JUMPING:     { ExtendFigureJump(); Anim.SetBool("Jumping", true); }     break;
            case JUMPING_STATE.FALLING:     { EndFigureJump(); } Anim.SetBool("Jumping", false); Anim.SetBool("Falling", true); break;
        }
    }

    void StartFigureJump()
    {
        if (Physics2D.OverlapCircle(m_feet.position, 0.3f, m_jump_flag_layer))
        {
            rb.velocity = Vector2.up * m_jump_force * /*Time.deltaTime*/ 0.016f;
            //Anim.SetBool("Jumping", true);
            jumping_state = JUMPING_STATE.JUMPING;
        }
    }

    void ExtendFigureJump()
    {
        jump_time_counter += Time.deltaTime;
        rb.velocity = Vector2.up * m_jump_force * /*Time.deltaTime*/ 0.016f;

        //Debug.Log("Time Counter: " + jump_time_counter + "Player Counter: " + player_controller.jump_time_counter);

        if (jump_time_counter > player_controller.jump_time_counter)
        {
            jump_time_counter = 0.0f;
            jumping_state = JUMPING_STATE.FALLING;
        }
    }

    void EndFigureJump()
    {
        if (normal_gravity_scale)
        {
            rb.gravityScale += 1.0f;
            normal_gravity_scale = false;
        }
        
        if (Physics2D.OverlapCircle(m_feet.position, ground_collision_radius, m_ground_layer))
        {
            if (!normal_gravity_scale)
            {
                rb.gravityScale -= 1.0f;
                normal_gravity_scale = true;
            }

            //Anim.SetBool("Jumping", false);
            jumping_state = JUMPING_STATE.GROUNDED;
        }
    }

    void InitVariables()
    {
        rb  = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();

        manager = FindObjectOfType<GameplayManager>();

        GameObject player   = GameObject.FindGameObjectWithTag("Player");
        player_transform    = player.transform;
        player_controller   = player.GetComponent<PlayerController>();

        position_behind_player = 1.0f /*+ Random.Range(0.0f, 4.0f)*/;

        transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }
}
