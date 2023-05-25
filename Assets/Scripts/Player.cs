using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class Player : MonoBehaviour
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

        //New input thing
        InputController inputController;

       
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

            GameObject gameManager = GameObject.Find("Game Manager");
            inputController = gameManager.GetComponent<InputController>();
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
                if (jumpPressed && !slidePressed)
                {
                    isGrounded = false;
                    avatarUp.enabled = true;
                    avatarDown.enabled = false;
                    animator.SetBool("Jump", true);
                    animator.SetBool("Slide", false);
                    velocityY = jumpVelocity;
                }
                else if (slidePressed)
                {
                    //slidePressed = false;
                    //Debug.Log("slideeeee");
                    avatarUp.enabled = false;
                    avatarDown.enabled = true;
                    animator.SetBool("Slide", true);
                }
                else if (!slidePressed)
                {
                    SlidingOver();
                }
            }
            else
            {
                animator.SetBool("Jump", true); ///hopefully
            }

        }

        public void SlidingOver()
        {

            animator.SetBool("Slide", false);
            Invoke("DelayColiderChange", 0.3f);
            //Debug.Log("SlidingOver");
            //slidePressed = false;


        }
        public void DelayColiderChange()
        {
            avatarUp.enabled = true;
            avatarDown.enabled = false;
            //Debug.Log("Not SLiding");
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

            // Testing air controls
            /*
            if (forwardPressed) //&& !isGrounded)
            {
                pos.z = pos.z + (5 * Time.deltaTime);
                //pos.z = Mathf.Clamp(pos.z, 3f, 6);

            }
            else
            {
                //pos.z = 4;
            }

            if (backPressed) //&& !isGrounded)
            {
                pos.z = pos.z - (5 * Time.deltaTime);
                //pos.z = Mathf.Clamp(pos.z, 3f, 6);

            }
            else
            {
                //pos.z = 4;
            }
            */
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
                if (slidePressed)
                {
                    //slidePressed = false;
                    defaultGravity = heavyGravity;
                }
            }


            if (animator.GetBool("Jump"))
            {
                if (slidePressed)
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
                inputController.controls.Gameplay.Disable();
                inputController.controls.InGameMenu.Enable();
                //Debug.Log("you dead LOL");
                Time.timeScale = 0;
            }

            transform.position = pos;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Barrier")
            {
                hp = hp - 1;
                frank = 0;
                //Debug.Log("Ouch Wall!!!");

                frank = 0;
                leftMovement *= 0.5f;
                other.gameObject.SetActive(false);
                //Destroy(other.gameObject);


            }

            if (other.gameObject.tag == "Spikes")
            {
                hp = hp - 2;
                //Debug.Log("Ouch Spikes!!!");
            }
        }
        // air control

      
           
       






        //input manager

        private bool slidePressed;
        private bool jumpPressed;
        private bool forwardPressed;
        private bool backPressed;


        public void OnSlide(bool isSlidePressed)
        {
            slidePressed = isSlidePressed;
            //Debug.Log("isSlidePressed " + isSlidePressed);
        }

        public void OnJump(bool isJumpPressed)
        {
            jumpPressed = isJumpPressed;
            //Debug.Log("wejumpin");
        }


        public void OnForward(bool isForwardPressed)
        {
            forwardPressed = isForwardPressed;
        }

        public void OnBack(bool isBackPressed)
        {
            backPressed = isBackPressed;
        }
    }
}