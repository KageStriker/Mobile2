using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cc;
    Animator anim;

    Vector3 moveDirection = Vector3.zero;

    public float moveSpeed;
    public float jumpForce;
    public float verticalVel;
    public float gravity;
    public float timeToDuck;
    public float timeToStand;

    CapsuleCollider capCol;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        capCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (cc.isGrounded)
        {
            anim.SetBool("Grounded", true);
            verticalVel = -gravity * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVel = jumpForce;
                anim.SetBool("isSliding", false);
            }
        }
        else
        {
            anim.SetBool("Grounded", false);
            verticalVel -= gravity * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Sliding", true);
            if(!cc.isGrounded)
            {
                verticalVel = -gravity;
            }
        }
        moveDirection.x = moveSpeed;

        moveDirection = new Vector3(moveSpeed, verticalVel, 0);

        cc.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(WaitForDeath());
        }
    }

    public void StandardCollider()
    {
        cc.height = 2.5f;
        capCol.height = 2.5f;
        anim.SetBool("Sliding", false);
    }

    public void SlidingCollider()
    {
        cc.height = 1f;
        capCol.height = 1.8f;
    }

    IEnumerator WaitForDeath()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
    }
}