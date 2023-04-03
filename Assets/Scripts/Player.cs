using UnityEngine;

public class Player : MonoBehaviour
{
    public float defaultGravity;
    float heavyGravity;
    public float jumpVelocity;
    float velocityY = 0;
    public float groundHeight;
    public bool isGrounded = true;
    Animator animator;
    public CapsuleCollider avatarUp;
    public CapsuleCollider avatarDown;
    float normalGravity;

    void Start()
    {
        heavyGravity = defaultGravity * 3;
        normalGravity = defaultGravity;
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isGrounded = false;
                avatarUp.enabled = true;
                avatarDown.enabled = false;
                animator.SetBool("Jump", true);
                animator.SetBool("Slide", false);
                velocityY = jumpVelocity;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                avatarUp.enabled = false;
                avatarDown.enabled = true;
                animator.SetBool("Slide", true);
            }
        }
    }

    public void SlidingOver()
    {
        Debug.Log("SlidingOver");
        avatarUp.enabled = true;
        avatarDown.enabled = false;
        animator.SetBool("Slide", false);
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (isGrounded == false)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                defaultGravity = heavyGravity;
            }
        }
        

        if (animator.GetBool("Jump"))
        {
            
            velocityY += defaultGravity * Time.fixedDeltaTime;
            pos.y += velocityY * Time.fixedDeltaTime;

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                animator.SetBool("Jump", false);
                isGrounded = true;
                velocityY = 0;
                defaultGravity = normalGravity;
            }
        }

        transform.position = pos;
    }
}
