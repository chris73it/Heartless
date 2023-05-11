using UnityEngine;

public class PlayerNew : MonoBehaviour
{
    public float defaultGravity;
    float heavyGravity;
    public float jumpVelocity;
    float velocityY = 0;
    public float groundHeight;
    float minGroundHeight = 0; // testing 
    public float ceilingHeight;
    public bool isGrounded = true;
    Animator animator;
    public CapsuleCollider avatarUp;
    public CapsuleCollider avatarDown;
    float normalGravity;
    public bool death;
    public float hp = 2; // default is 2
    public GameObject button;
    public float frank = 0; //hp manager


    //things from level manager
    Vector3 leftMovement;
    Vector3 leftThreshold;
    Vector3 rightThreshold;
    public GameObject[] backgroundPrefabs;
    GameObject level;
    Quaternion rotation;

   
    
    void Start()
    {

        rotation = Quaternion.Euler(0, 90, 0);

        level = GameObject.Find("Level");
        var backgroundManager = level.GetComponent<BackgroundManager>();
        leftMovement = backgroundManager.leftMovement;
        leftThreshold = backgroundManager.leftThreshold;
        rightThreshold = backgroundManager.rightThreshold;
        backgroundPrefabs = backgroundManager.backgroundPrefabs;



        heavyGravity = defaultGravity * 3;
        normalGravity = defaultGravity;
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        frank = frank + (1 * Time.deltaTime);
        if (frank >= 3)
        {
            //Debug.Log("Hi Frank"); // good job frank!!!

            frank = 0;
            hp = 2;
        }

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



         // testing for pitfalls
        // Bit shift the index of the layer (11) to get a bit mask
        int layerMask2 = 1 << 11; //1 << 11;

        // This would cast rays only against colliders in layer 11.
        // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit2;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit2, 6, layerMask2))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit2.distance, Color.green);
           // Debug.Log("Did Hit");
            minGroundHeight = 0;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 6, Color.cyan);
            //Debug.Log("Did not Hit");
            minGroundHeight = -6;
        }

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
            //Debug.Log("Did Hit");
            groundHeight = 3.51f;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 5, Color.white);
            //Debug.Log("Did not Hit");
            groundHeight = minGroundHeight;
        }


        ///////////testtiem
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 5, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.red);
           // Debug.Log("Did Hit");
            ceilingHeight = 1.42f;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 5, Color.white);
            //Debug.Log("Did not Hit");
            ceilingHeight = 5.95f;
        }





       




        Vector3 pos = transform.position;

        //testing 5testing 124
        if (pos.y != groundHeight)
        {
            isGrounded = false;
        }

        if (pos.y >= ceilingHeight)
        {
            pos.y = ceilingHeight;
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
            if (Input.GetKeyDown(KeyCode.S))
            {
                defaultGravity = heavyGravity;
            }

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

        // death checks
        if (pos.y < -4)
        {
            death = true;
        }
        if (hp <= 0)
        {
            death = true;
        }
        if (death == true)
        {
            ///place dead things here
            button.SetActive(true);
            //Debug.Log("you dead LOL");
            Time.timeScale = 0;
        }

        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag  == "Barrier")
        {
            hp = hp - 1;
            frank = 0;
            //Debug.Log("Ouch Wall!!!");

            frank = 0;
            leftMovement *= 0.5f;
            Destroy(other.gameObject);
            

        }

        if (other.gameObject.tag == "Spikes")
        {
            hp = hp - 2;
            //Debug.Log("Ouch Spikes!!!");
        }
    }
}
