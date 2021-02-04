using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRef : MonoBehaviour
{

    public float jump_force;
    public bool is_jumping;
    public bool is_crouched;
    public bool is_dead;
    private Rigidbody2D my_rb;
    private Animator my_anim;
    //public LevelManager level_manager;
    public BoxCollider2D head_collider;
    public BoxCollider2D crouch_collider;

    public AudioSource jump_sound;
    public bool is_marta;
    

    void Start()
    {
        is_jumping = false;
        is_crouched = false;
        is_dead = false;
        my_rb = GetComponent<Rigidbody2D>();
        my_anim = GetComponent<Animator>();
        Debug.Log("Hello Dino World");
    }

    void Update()
    {
        //is_dead = level_manager.game_over;
        SetAnimator();
        InputManager();
        SetColliders();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collider)
    {
        // Debug.Log("Collission");
        if (collider.gameObject.tag == "Ground")
        {
            is_jumping = false;

        }
        if (collider.gameObject.tag == "Respawn")
        {
            //level_manager.GameOver();
        }
    }

    public void SetAnimator()
    {
        if (is_dead)
        {
            my_anim.SetBool("is_dead", is_dead);
        }
        else
        {
            my_anim.SetBool("is_jumping", is_jumping);
            my_anim.SetBool("is_crouched", is_crouched);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !is_jumping && !is_crouched)
        {
            my_rb.velocity = new Vector3(0, jump_force, 0);
            is_jumping = true;
            jump_sound.Play();
        }
    }

    public void Crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (is_jumping)
            {
                my_rb.velocity = new Vector3(0, -jump_force * 0.8f, 0);
            }
            else
            {
                is_crouched = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            is_crouched = false;
        }
    }

    public void SetColliders()
    {
        if (is_crouched)
        {
            crouch_collider.enabled = true;
            head_collider.enabled = false;
        }
        else
        {
            crouch_collider.enabled = false;
            head_collider.enabled = true;
        }
    }

    public void InputManager()
    {
        if (!is_dead)
        {
            Jump();
            Crouch();

        }
        else
        {
            Revive();
        }
    }

    public void Revive()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            //level_manager.Restart();
        }
    }


}
