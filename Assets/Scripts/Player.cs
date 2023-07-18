using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class Player : MonoBehaviour
    {

        //debug mode
        public bool deBugModeInvuln;

        public int score;
        float scoresaver;
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
        public int hp = 2; // default is 2
        public int  maxHP = 2;
        public GameObject button;
        //public BackgroundManager backgroundM;
        public Vector3 NewleftMovement;

      

        public float minSpeed = -0.1f;
        public float maxSpeed = -0.2f;

        public float frank = 0; //hp manager

        public int woodrand = 1; // randomizes wood break audio on collision 
        public int steprand = 1; // randomizes step audio 
        public int landrand = 1; // randomizes landing sounds (could amybe be used for jumping too)
        int barrierDamage;

        //audio
        public AudioSource impactOfSound;
        public AudioSource damage;
        public AudioSource spikeDeath;
        public AudioSource wallDeath;
        public AudioSource fallDeath;
        public AudioSource step;
        public AudioSource slideAudio;
        public AudioSource landAudio;
        public AudioSource JumpAudio;
        public AudioSource JumpWhooshAudio;
        public AudioSource fallDeathYell;



        //particle systems
        ParticleSystem pstomp;
         ParticleSystem wallBuster;
         ParticleSystem phead;
         ParticleSystem pslide;
         ParticleSystem pwind;
         ParticleSystem pMaxWind;
        ParticleSystem deathDust;

        // cam shake
        public GameObject cameraManager;
       

        //New input thing
        InputController inputController;

       
        void Start()
        {
            NewleftMovement = new Vector3(0, 0, maxSpeed);
            barrierDamage = 1;
           
            //particle systems
            pstomp = GameObject.Find("ground_stomp").GetComponent<ParticleSystem>();
            wallBuster = GameObject.Find("wall_breaker").GetComponent<ParticleSystem>();
            phead = GameObject.Find("HeadBanger").GetComponent<ParticleSystem>();
            pslide = GameObject.Find("slideEffect").GetComponent<ParticleSystem>();
            pwind = GameObject.Find("speed trail").GetComponent<ParticleSystem>();
            pMaxWind = GameObject.Find("speed trail MAX").GetComponent<ParticleSystem>();
            deathDust = GameObject.Find("DeathDust").GetComponent<ParticleSystem>();



            deathDust.Stop();
            pstomp.Stop();
            wallBuster.Stop();
            phead.Stop();
            pMaxWind.Play();



            score = 0;
            scoresaver = score;


            heavyGravity = defaultGravity * 3;
            normalGravity = defaultGravity;

            animator = GetComponent<Animator>();

            GameObject gameManager = GameObject.Find("Game Manager");
            inputController = gameManager.GetComponent<InputController>();
        }

        void Update()
        {
            scoresaver = (scoresaver + (1 * Time.deltaTime) * (-1 * NewleftMovement.z) * 35); // score in meters?
            score = (int)scoresaver;

            frank = frank + (1 * Time.deltaTime);
            if (frank >= 3)
            {
                //Debug.Log("Hi Frank"); // good job frank!!!

                frank = 0;
                hp = hp + 1;
                if(hp >= maxHP)
                {
                    hp = maxHP;
                }
            }

          
            //changing barrier damage based on speed if too slow
            if (NewleftMovement.z >= minSpeed - 0.035f && death == false)
            {
               
                cameraManager.GetComponent<CameraShake>().ClosingInOnYou(0.2f);
                barrierDamage = 2;
            }
            else
            {
                if (cameraManager.GetComponent<CameraShake>().isClosing == true)
                {
                    cameraManager.GetComponent<CameraShake>().ShakeSchtop();
                    cameraManager.GetComponent<CameraShake>().isClosing = false;
                    
                }
                barrierDamage = 1;
            }


            // regaining speed
            if (NewleftMovement.z >= maxSpeed)
            {
                NewleftMovement.z = NewleftMovement.z - 0.02f * Time.deltaTime;
                if (NewleftMovement.z <= maxSpeed)
                {
                    NewleftMovement = new Vector3(0, 0, maxSpeed);
                    
                }
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
                    JumpAudio.PlayOneShot(JumpAudio.clip);
                    JumpWhooshAudio.PlayOneShot(JumpWhooshAudio.clip);
                    //animator.SetBool("Running Slide", false); //28
                    velocityY = jumpVelocity;
                }
                else if (slidePressed)
                {
                    //slidePressed = false;
                    //Debug.Log("slideeeee");
                    avatarUp.enabled = false;
                    avatarDown.enabled = true;
                    animator.SetBool("Slide", true);
                    pslide.Play();
                    
                    


                    //Slide slows you down over time

                    NewleftMovement.z = NewleftMovement.z + 0.03f * Time.deltaTime;
                    if (NewleftMovement.z >= minSpeed - 0.01f)
                    {
                        NewleftMovement = new Vector3(0, 0, minSpeed - 0.01f);
                       
                    }                
                    if (maxSpeed <= -0.2f)
                    {
                        if (NewleftMovement.z >= maxSpeed)
                        {
                            maxSpeed = -0.2f;
                        }
                    }

                    
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

            // wind effects
            if (NewleftMovement.z <= -0.235f)// && !slidePressed)
            {
                maxHP = 3;
                pwind.Play();
                //Debug.Log("pwind");
            }
            else
            {
                maxHP = 2;
                pwind.Stop();
            }

            var PMaxEmission = pMaxWind.emission;
            if (NewleftMovement.z <= -0.275f && !slidePressed)
            {
                maxHP = 4;
                //pMaxWind.Play();
                //Debug.Log("pmaxwind");

                PMaxEmission.enabled = true;
            }
            else
            {

                //pMaxWind.Stop();
                PMaxEmission.enabled = false;
            }

        }

        public void SlidingOver()
        {
            pslide.Stop();
           
            animator.SetBool("Slide", false);
            Invoke("DelayColiderChange", 0.35f);
            slideAudio.mute = true;
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
            else if(deBugModeInvuln == false)
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
            //changing air controls to speed controls
            animator.SetFloat("RunningSpeed", NewleftMovement.z * -5f);// testing animation speed
            if (animator.GetFloat("RunningSpeed") == 0)
            {
                animator.SetFloat("RunningSpeed", 1);
            }

            if (forwardPressed && (animator.GetBool("Slide") == false)) //&& !isGrounded)
            {
                
                if (NewleftMovement.z <= maxSpeed)
                {
                    maxSpeed = maxSpeed - 0.05f * Time.deltaTime;
                }
               
               if (maxSpeed <=  -0.30f)
                {
                    maxSpeed = -0.30f;
                }
              

            }
            

            if (backPressed) //&& !isGrounded)
            {
               
                maxSpeed = maxSpeed + 0.05f * Time.deltaTime;
                if (maxSpeed >= -0.2f)
                {
                    maxSpeed = -0.2f;
                }
                if (maxSpeed <= -0.2f)
                {
                    
                    if (NewleftMovement.z <= maxSpeed && !slidePressed)
                    {
                        NewleftMovement.z = NewleftMovement.z + 0.05f * Time.deltaTime;
                      
                        if (NewleftMovement.z > maxSpeed)
                        {
                            NewleftMovement.z = maxSpeed;
                        }
                       
                      
                        
                    }
                }
            }
          

            if (NewleftMovement.z > minSpeed)
            {
                NewleftMovement.z = minSpeed;
            }
            //testing 5testing 124
            if (pos.y != groundHeight)
            {
                isGrounded = false;
            }

            if (pos.y >= ceilingHeight)
            {
                phead.Play();

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


            if (animator.GetBool("Slide") == true)
            {
                slideAudio.mute = false;
                slideAudio.pitch = Random.Range(0.17f, 0.25f);
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
                    pstomp.Play();
                    // play landign audio
                    landrand = Random.Range(0, landAudio.GetComponent<landingRandAud>().soundStore.Length);
                    landAudio.GetComponent<landingRandAud>().LandingPitchRand();
                    landAudio.PlayOneShot(landAudio.clip);
                    pos.y = groundHeight;
                    animator.SetBool("Jump", false);
                    isGrounded = true;
                    velocityY = 0;
                    defaultGravity = normalGravity;
                }
            }



            // death checks
            if (pos.y <= -0.5f)
            {
                landAudio.mute = true;
                animator.SetBool("FallDeath", true);
               
                death = true;
            }
            if (hp <= 0 && deBugModeInvuln == false)
            {
                death = true;
                hp = 0;
            }
            if (death == true && deBugModeInvuln == false)
            {
                ///place dead things here
                step.mute = true;
                frank = 0;
                button.SetActive(true);
                animator.SetBool("DeadCheck", true);
                inputController.controls.Gameplay.Disable();
                inputController.controls.InGameMenu.Enable();
                //Debug.Log("you dead LOL");
                NewleftMovement.z = 0.0f;
                minSpeed = 0.0f;
                maxSpeed = 0.0f;
               
            }
            if (death == true && cameraManager.GetComponent<CameraShake>().isShaking == false && pos.y <= -0.5)
            {
                fallDeathYell.mute = false;
                //fallDeathYell.Play();


            }

            if (death == true && cameraManager.GetComponent<CameraShake>().isShaking == false && pos.y > -0.1 )
            {
                cameraManager.GetComponent<CameraShake>().ShakeSchtop();
                cameraManager.GetComponent<CameraShake>().DeathShakeStart();
               // Debug.Log("shakeyshake");
            }
            else if (death == true && cameraManager.GetComponent<CameraShake>().isShaking == false && pos.y <= -5.8f)
            {
                
                fallDeath.Play();
                deathDust.Play();
                cameraManager.GetComponent<CameraShake>().DeathShakeStart();
                hp = 0; //here to update hp after falling
                fallDeathYell.mute = true;
                //Debug.Log("AAAAAAAAAAHHHHHHHHHHH! IM FALLING!!!!");
            }

                transform.position = pos;
        }
        private void OnTriggerEnter(Collider other)
        {
           

            if (other.gameObject.tag == "Barrier")
            {
               
               

                wallBuster.Play();
                
                cameraManager.GetComponent<CameraShake>().ShakeSchtop();
                cameraManager.GetComponent<CameraShake>().ShakeStart();
                //frank = 0;
                //Debug.Log("Ouch Wall!!!");
                if ( deBugModeInvuln == false)
                {
                    hp = hp - barrierDamage;
                    NewleftMovement = new Vector3(0, 0, minSpeed);
                    animator.SetFloat("RunningSpeed", 0.6f);
                    maxSpeed = -0.2f; //resets max speed
                }
                

                frank = 0;
               
                if (hp <= 0 && deBugModeInvuln == false)
                {
                    animator.SetBool("BarrierDeath", true);
                    wallDeath.Play();
                    
                }
                else if(deBugModeInvuln == false)
                {
                    damage.PlayOneShot(damage.clip);
                }

                other.gameObject.SetActive(false);
                impactOfSound.GetComponent<WoodAudRandomizer>().PitchRand();

                impactOfSound.PlayOneShot(impactOfSound.clip);
                woodrand = Random.Range(0, impactOfSound.GetComponent<WoodAudRandomizer>().woodsounds.Length);


            }

            if (other.gameObject.tag == "Spikes" && deBugModeInvuln == false)
            {
                hp = hp - 2;
                frank = 0;
                cameraManager.GetComponent<CameraShake>().ShakeSchtop();
                cameraManager.GetComponent<CameraShake>().ShakeStart();
                //Debug.Log("Ouch Spikes!!!");
                if (hp <= 0 )
                {
                    animator.SetBool("SpikeDeath", true);
                    spikeDeath.Play();
    }
                else
                {
                    woodrand = Random.Range(0, impactOfSound.GetComponent<WoodAudRandomizer>().woodsounds.Length);
                    animator.SetBool("StubbedToe", true);
                    damage.PlayOneShot(damage.clip);

                }
                //animator.SetBool("StubbedToe", false);
            }
        }

        public void StumbleEnd()
        {
            animator.SetBool("StubbedToe", false);
        }
        public void FootStep()
        {
            step.PlayOneShot(step.clip);

            steprand = Random.Range(0, step.GetComponent<Footstepper>().stepsounds.Length);
        }
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