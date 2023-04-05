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
        else
        {
            animator.SetBool("Jump", true); ///hopefully
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





        // Bit shift the index of the layer (10) to get a bit mask
        int layerMask = 1 << 10;

        // This would cast rays only against colliders in layer 10.
        // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5, layerMask)) 
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.blue);
            Debug.Log("Did Hit");
            groundHeight = 3.51f;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 5, Color.white);
            Debug.Log("Did not Hit");
            groundHeight = 0;
        }



       





            Vector3 pos = transform.position;

        //testing 5testing 124
        if (pos.y != groundHeight)
        {
            isGrounded = false;
        }




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
