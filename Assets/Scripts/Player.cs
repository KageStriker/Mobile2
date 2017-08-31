using System.Collections;
using UnityEngine;

public enum LifeState
{
    Alive,
    Dead
}

public class Player : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;

    private Vector3 moveDirection = Vector3.zero;

    public float moveSpeed;
    public float jumpForce;
    public float verticalVel;
    public float gravity;

    protected float health = 100;

    protected LifeState lifeState;

    CapsuleCollider capCol;

    Touch touch;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        capCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        moveSpeed += 0.05f * Time.deltaTime;

        if (Input.touchSupported && Input.touchCount > 0)
        {
            switch (lifeState)
            {
                case LifeState.Alive:
                    if (cc.isGrounded)
                    {
                        anim.SetBool("Grounded", true);
                        verticalVel = -gravity * Time.deltaTime;

                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            verticalVel = jumpForce;
                            anim.SetBool("Sliding", false);
                        }
                    }
                    else
                    {
                        anim.SetBool("Grounded", false);
                        verticalVel -= gravity * Time.deltaTime;
                    }

                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        anim.SetBool("Sliding", true);
                        if (!cc.isGrounded)
                        {
                            verticalVel = -(0.5f * gravity);
                        }
                    }

                    moveDirection.x = moveSpeed;

                    moveDirection = new Vector3(moveSpeed, verticalVel, 0);

                    cc.Move(moveDirection * Time.deltaTime);
                    break;
                case LifeState.Dead:

                    break;
            }
        }
        else
        {
            switch (lifeState)
            {
                case LifeState.Alive:
                    if (cc.isGrounded)
                    {
                        anim.SetBool("Grounded", true);
                        verticalVel = -gravity * Time.deltaTime;

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            verticalVel = jumpForce;
                            anim.SetBool("Sliding", false);
                        }
                    }
                    else
                    {
                        anim.SetBool("Grounded", false);
                        verticalVel -= gravity * Time.deltaTime;
                    }

                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        anim.SetBool("Sliding", true);
                        if (!cc.isGrounded)
                        {
                            verticalVel = -(0.5f * gravity);
                        }
                    }

                    moveDirection.x = moveSpeed;

                    moveDirection = new Vector3(moveSpeed, verticalVel, 0);

                    cc.Move(moveDirection * Time.deltaTime);
                    break;
                case LifeState.Dead:

                    break;
            }
        }
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
        anim.SetBool("Sliding", false);
        cc.height = 2.5f;
        capCol.height = 2.5f;
    }

    public void SlidingCollider()
    {
        cc.height = 1f;
        capCol.height = 1.8f;
    }

    public void SetDead()
    {
        lifeState = LifeState.Dead;
    }

    public void SetAlive()
    {
        lifeState = LifeState.Alive;
    }

    IEnumerator WaitForDeath()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
    }
}