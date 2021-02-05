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
    [HideInInspector] public JUMPING_STATE  jumping_state   = JUMPING_STATE.FALLING;

    public LayerMask            m_ground_layer;
    public LayerMask            m_hazards_layer;

    public Transform            m_feet;
    public float                ground_collision_radius     = 0.1f;
    public float                player_collision_radius     = 0.5f;

    public float                m_jump_force                = 350.0f;
    public float                m_jump_time                 = 0.4f;

    private Rigidbody2D         rb;
    private CapsuleCollider2D   col;
    private Vector2             jump_velocity               = Vector2.zero;
    private float               jump_time_counter           = 0.0f;
    private bool                jump_released               = false;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFigureState();

        if (FigureIsActive())
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

        if (CheckForPlayerPickUp())
        {
            figure_state = FIGURE_STATE.ACTIVE;
        }
    }

    bool CheckForFigureDeath()
    {
        return false;
    }

    bool CheckForPlayerPickUp()
    {
        return false;
    }

    bool FigureIsActive()
    {
        return (figure_state == FIGURE_STATE.ACTIVE && figure_state != FIGURE_STATE.DEAD);
    }

    void UpdateJumpingState()
    {
        switch (jumping_state)
        {
            case JUMPING_STATE.GROUNDED:    { StartFigureJump(); }      break;
            case JUMPING_STATE.JUMPING:     { ExtendFigureJump(); }     break;
            case JUMPING_STATE.FALLING:     { EndFigureJump(); }        break;
        }
    }

    void StartFigureJump()
    {

    }

    void ExtendFigureJump()
    {

    }

    void EndFigureJump()
    {

    }

    void InitVariables()
    {

    }
}
