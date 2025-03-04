using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeroicArcade.CC.Core
{

   
    public class FarmerController : MonoBehaviour
    {
        
        public float defaultGravity;
  
        public float jumpVelocity;
       // float velocityY = 0;
        public float groundHeight;
   
        public bool isGrounded = true;

        float newfloat;
        //float normalGravity;


        //audio

        public AudioSource rumblinging;
        public AudioSource Chase;

        public bool armed;
        bool randBool;
        float randFloat;

        public GameObject level;
        Vector3 leftMovement;
        Vector3 pos;
        float maxForward;
        float maxBack;

        float maxChaseVol = 0.381f;
        float maxRumbleVol= 0.5f;
        //float checkpos; // checks position so if farmer doesnt jump they still celebrate

        Animator animator2;
        // Start is called before the first frame update


        void Start()
        {
            newfloat = Random.Range(0.7f, 1.3f);
           
            animator2 = GetComponent<Animator>();

           pos = transform.position;

           
            maxBack = pos.z = pos.z - 5f;
            maxForward = pos.z = pos.z  +5f;

            //pos.z = maxBack;
            animator2.SetBool("HoldingSomething", armed);
            animator2.SetBool("Running", true);
        }

        // Update is called once per frame
        void Update()
        {
            var backgroundManager = level.GetComponent<BackgroundManager>();
            leftMovement = backgroundManager.leftMovement;

            int layerMask2 = 1 << 11; //1 << 11;

            //Debug.Log(pos.z);
            
            animator2.SetFloat("RunningVariation", newfloat);

            randFloat = Random.Range(1, 7);
            if (randFloat > 4)
            {
                randBool = true;
            }
            else
            {
                randBool = false;
            }
            animator2.SetBool("randVariation", randBool);

            

            // This would cast rays only against colliders in layer 11.
            // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.
            //layerMask = ~layerMask;

            RaycastHit hit3;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit3, 6, layerMask2))
            {
                if (hit3.collider.tag == "FarmerCheck")
                {
                    jumping();
                }
                else
                {
                    animator2.SetBool("Jump", false);
                    //animator2.SetBool("Running", true);
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit3.distance, Color.green);
                // Debug.Log("Did Hit");
            }  /* 
          else
          {
              Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 6, Color.cyan);
              //Debug.Log("Did not Hit");

              animator2.SetBool("Jump", false);

          }
            */

            //checkpos = pos.z;
            SpeedCheck();
            transform.position = pos;

        }

        void jumping()
        {
            newfloat = Random.Range(0.7f, 1.3f);
            animator2.SetBool("Jump", true);
            //animator2.SetBool("Running", false);
        }
        void SpeedCheck()
        {
            if (leftMovement.z >= -0.19f)
            {
                rumblinging.volume = rumblinging.volume + 0.03f * Time.deltaTime;
                Chase.volume = Chase.volume + 0.03f * Time.deltaTime;
                if (Chase.volume >= maxChaseVol)
                {
                    Chase.volume = maxChaseVol;
                }
                if (rumblinging.volume >= maxRumbleVol)
                {
                    rumblinging.volume = maxRumbleVol;
                }

                pos.z = pos.z +( 2.2f * Time.deltaTime);
                if (leftMovement.z >= 0)
                {
                    
                  
                    pos.z = pos.z + (2.8f * Time.deltaTime);
                    if (animator2.GetBool("Jump") == true)
                    {

                        maxForward = maxForward + (3.5f* Time.deltaTime);
                      
                        if (pos.z == maxForward)
                        {
                            animator2.SetBool("Running", false);
                        }

                            //animator2.SetBool("Caught Dronion", true);
                    }
                }
                if (pos.z >= maxForward)
                {
                    pos.z = maxForward;
                    animator2.SetBool("Running", false);

                    rumblinging.volume = maxRumbleVol;
                    Chase.volume = maxChaseVol;
                }
                else
                {
                    animator2.SetBool("Running", true);
                }

                CatchCheck();
            }
            else
            {
                rumblinging.volume = rumblinging.volume - 0.01f * Time.deltaTime;
                Chase.volume = Chase.volume - 0.01f * Time.deltaTime;
                if (Chase.volume <= 0)
                {
                    Chase.volume = 0;
                }
                if (rumblinging.volume <= 0)
                {
                    rumblinging.volume = 0;
                }

                pos.z = pos.z - (3.0f * Time.deltaTime);
                if (pos.z <= maxBack)
                {
                    pos.z = maxBack;
                }

            }
        }
        void CatchCheck()
        {
            if (animator2.GetBool("Jump") == false && animator2.GetBool("Running") == false)//(checkpos == pos.z)
            {
                if (leftMovement.z >= 0)
                {
                    //animator2.SetBool("Running", false);
                    animator2.SetBool("Caught Dronion", true);
                   
                }

                //Debug.Log("fixMer" + checkpos+ "     uikfahnawshf;   " + pos.z);
            }
            if (animator2.GetBool("Caught Dronion") == true && animator2.GetBool("Running") == false)
            {
                rumblinging.mute = true;
                Chase.mute = true;
            }
          
        }

    }
}