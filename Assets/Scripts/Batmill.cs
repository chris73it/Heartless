using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class Batmill : MonoBehaviour
    {
        Vector3 leftMovement;
        Vector3 leftThreshold;
        Vector3 rightThreshold;
        //public GameObject[] backgroundPrefabs;
        GameObject level;
        Quaternion rotation;
        Animator animatorWho;
        public GameObject bat;

        float poyo; // stores random val for speed modulation
        int spoopy; // visivilty variablle

        

        void Start()
        {
            // rotation = Quaternion.Euler(0, 90, 0);
            //bat = GameObject.Find("Level");
            level = GameObject.Find("Level");
            var backgroundManager = level.GetComponent<BackgroundManager>();
            leftMovement = backgroundManager.leftMovement;
            leftThreshold = backgroundManager.leftThreshold;
            rightThreshold = backgroundManager.rightThreshold;
            //backgroundPrefabs = backgroundManager.backgroundPrefabs;

           
            poyo = Random.Range(0.05f, 0.1f);

            leftMovement = backgroundManager.leftMovement;
            leftMovement.z = leftMovement.z -poyo;

            animatorWho = GetComponent<Animator>();
            animatorWho.SetFloat("Wublespeed", Random.Range(0.8f, 1.5f));

            spoopy = 1;
        }

        void FixedUpdate()
        {//????????? update/ fixedupdate
            var backgroundManager = level.GetComponent<BackgroundManager>();
            leftMovement = backgroundManager.leftMovement;
            leftMovement.z = leftMovement.z - poyo;
            if (spoopy >1)
            {
                bat.SetActive(false);
            }
            else
            {
                bat.SetActive(true);
            }

            transform.position += leftMovement;// * Time.deltaTime;//?????????

            if (transform.position.z <= leftThreshold.z)
            {
                spoopy = spoopy + 1;
                if (spoopy >7)
                {
                    spoopy = 1;
                }
               
                //int index = Random.Range(0, backgroundPrefabs.Length);
               
                poyo = Random.Range(0.05f, 0.1f);
               
                animatorWho.SetFloat("Wublespeed", Random.Range(0.8f, 1.5f));

                //spoopy = Random.Range(1, 6);
                transform.position = transform.position + new Vector3(0, 0, 40);//rightThreshold;
                                                                                // the vector 3 with the value of 40 moves the platforms instead of teleporting them in order to plug gaps
            }

        }
    }
}