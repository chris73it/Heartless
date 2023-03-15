using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector3 velocity;
    public float jumpVelocity = 20;
    public float groundHeight = 0;
    public bool isGrounded = false;
    Animator animator;
    public CapsuleCollider avatarUp;
    public CapsuleCollider avatarDown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isGrounded = false;
                //avatarDown.disabled = !avatarDown.enabled;
                avatarUp.enabled = true;
                avatarDown.enabled = false;
                animator.SetBool("Jump", true);
                animator.SetBool("Slide", false);
                velocity.y = jumpVelocity;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                avatarUp.enabled = false;
                avatarDown.enabled = true;
                animator.SetBool("Slide", true);
            }
        }
        else
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
        }
    }

    public void SlidingOver()
    {
        Debug.Log("lidingOver");
        avatarUp.enabled = true;
        avatarDown.enabled = false;
        animator.SetBool("Slide", false);
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        if (isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
           velocity.y += gravity * Time.fixedDeltaTime;

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                //isGrounded = true;
            }
        }

        transform.position = pos;
    }

}
