using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    CharacterController cc;
    Rigidbody rb;
    Animator anim;

    Transform playerTransform;
    Vector3 moveDirection = Vector3.zero;
    
    private float speed;
    private bool jump;
    private float jumpSpeed;
    private float gravity;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();

        speed = 10;
        jumpSpeed = 30f;
        gravity = 9.81f;

        anim.SetBool("isMoving", true);
    }

    private void Update()
    {
        GroundCheck();
        
        moveDirection = transform.forward;
        moveDirection *= speed;

        Vector3 newVel = transform.forward * jumpSpeed;
        newVel.y = rb.velocity.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cc.isGrounded)
            {
                Debug.Log("Jump");

                newVel.y = -jumpSpeed;
            }
        }

        if (cc.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime; 
        }

        cc.Move(moveDirection * Time.deltaTime);
    }

    private void GroundCheck()
    {
        if (cc.isGrounded)
        {
            anim.SetBool("grounded", true);
        }
        else
        {
            anim.SetBool("grounded", false);
        }
    }

    private void ResetPlayer()
    {
        float currentY = playerTransform.position.y;
        playerTransform.position = new Vector3(-25, 0, 0);
    }
}
