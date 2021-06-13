using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    float HorizontalMove = 0f;
    float VerticalMove = 0f;
    bool jump = false;
    bool jumping = false;
    public Animator animator;
    bool crouch = false;
    bool crouching = false;
    public float RunSpeed = 80f;
    public AudioSource jumpsnd;

    // Update is called once per frame
    void Update()
    {
        

        VerticalMove = Input.GetAxisRaw("Vertical");
        HorizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;

        animator.SetBool("Jumping", jumping);
        animator.SetBool("Crouching", crouching);
        animator.SetFloat("Speed", HorizontalMove);

        if (transform.position.y < -23f)
        {
            transform.position = new Vector3(-2.18f, -11.78f, 0);
        }

        if ((VerticalMove > 0.8 || Input.GetButtonDown("Jump")) && !jumping)
        {
            jump = true;
            jumping = true;
            jumpsnd.transform.position = transform.position;
            jumpsnd.Play();
            
        }
        if (VerticalMove < -0.8 || Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch") || VerticalMove > -0.8)
        {
            crouch = false;
        }
    }
    void FixedUpdate()
    {
        controller.Move(HorizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    public void OnLanding()
    {
        jumping = false;
        jumpsnd.Stop();
    }
    public void OnCrouch(bool IsCrouching)
    {
        crouching = IsCrouching;
    }
}

