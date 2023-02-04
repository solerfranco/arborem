using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    public float moveSpeed;
    public ParticleSystem dustParticles;

    private Rigidbody rb;
    private SpriteRenderer sprite;
    private PlayerInputActions playerInputActions;
    private Animator anim;

    private Vector2 inputDirection = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private bool IsMoving => inputDirection != Vector2.zero;

    private void Move()
    {
        inputDirection = playerInputActions.Player.Movement.ReadValue<Vector2>(); //Read movement input from controller
        if (inputDirection.x != 0) //If Character is moving
        {
            sprite.flipX = inputDirection.x < 0; //Flip Character if moving to the left
        }
        anim.SetBool("Moving", IsMoving); //Set animation to move or idle
        rb.velocity = new Vector3(inputDirection.x * moveSpeed, rb.velocity.y, inputDirection.y * moveSpeed); //Apply movement to the character
    }

    private void Update()
    {
        CheckParticles();
    }

    private void CheckParticles()
    {
        if (IsMoving)
        {
            if (!dustParticles.isEmitting)
            {
                dustParticles.enableEmission = true;
                float tinyStep = 0.000001f;
                dustParticles.Simulate(tinyStep, true, true, false);
                dustParticles.Play();
            }
        }
        else if(dustParticles.isPlaying)
        {
            dustParticles.enableEmission = false;
            //dustParticles.Pause();
        }
    }
}
