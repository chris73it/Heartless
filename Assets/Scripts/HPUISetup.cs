using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class HPUISetup : MonoBehaviour
    {
        public GameObject Heart1;
        public GameObject Heart2;
        public GameObject Heart3;
        public GameObject Heart4;

        public GameObject DeadHeart1;
        public GameObject DeadHeart2;
        public GameObject DeadHeart3;
        public GameObject DeadHeart4;

        Vector3 heartoriginalPosition4;
        Vector3 heartoriginalPosition3;
        Vector3 heartoriginalPosition2;
        Vector3 heartoriginalPosition1;

        private Vector3 heart1pos;
      
        public GameObject playerobj;
        private Player player;
        int MaxHP = 2;
        int HP = 2;
        float speedIndicator;
        void Start()
        {
           heartoriginalPosition4 = Heart4.transform.localPosition;
           heartoriginalPosition3 = Heart3.transform.localPosition;
           heartoriginalPosition2 = Heart2.transform.localPosition;
           heartoriginalPosition1 = Heart1.transform.localPosition;
            // playerobj = GameObject.Find("Dronion");
            player = playerobj.GetComponent<Player>();
            Debug.Log(player.name);

        }
        //private Vector3 originalPosition;
        public float shakeTime = 0.3f;
        public float shakeMag = 100000f;
        


        public IEnumerator hpshake(float duration, float magnitude, GameObject heart)
        {
            Vector3 originalPosition;
            originalPosition = heart.transform.localPosition;
            
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
              
                float y = Random.Range(-100f, 100f) * magnitude;

                heart.transform.localPosition = originalPosition + new Vector3(0f, y, 0f);
                heart.transform.localPosition = new Vector3(originalPosition.x, Mathf.Clamp(heart.transform.localPosition.y, -10f, 10f),originalPosition.z); 
              elapsedTime += Time.deltaTime;

                yield return null;
            }

            heart.transform.localPosition = originalPosition;

           

        }
        public void HPRumble(GameObject heart)
        {
            StartCoroutine(hpshake(shakeTime, shakeMag, heart));
           
        }
        public void RumbleEnd(GameObject heart)
        {
            StopCoroutine(hpshake(shakeTime, shakeMag, heart));
            //transform.localPosition = originalPosition;
        }
        public void PosReset(int index)
        {
            if (index == 1)
            {
                Heart1.transform.localPosition = heartoriginalPosition1;
            }
           else if (index == 2)
            {
                Heart2.transform.localPosition = heartoriginalPosition2;
            }
            else if (index == 3)
            {
                Heart3.transform.localPosition = heartoriginalPosition3;
            }
            else if(index == 4)
            {
                Heart4.transform.localPosition = heartoriginalPosition4;
            }
           
           
           
          
        }
       
        // Update is called once per frame
        void Update()
        {
            HP = player.hp;
            MaxHP = player.maxHP;
            speedIndicator = player.NewleftMovement.z;
            //test rumble
            // HPRumble(Heart2);
            //HPRumble(Heart1);
            if (speedIndicator >= ( -0.15 - 0.03f) && player.death == false)
                {
                HPRumble(Heart4);
                PosReset(4);
                HPRumble(Heart3);
                PosReset(3);
                HPRumble(Heart2);
                PosReset(2);
                HPRumble(Heart1);
                PosReset(1);
            }
            else if(HP == 1)
            {
                HPRumble(Heart4);
                PosReset(4);
                HPRumble(Heart3);
                PosReset(3);
                HPRumble(Heart2);
                PosReset(2);
                HPRumble(Heart1);
                PosReset(1);
              
            }
            else
            {
                PosReset(2);
                PosReset(1);
            }

            if (MaxHP >= 1)
            {
                Heart1.SetActive(true);
               
            }
            else
            {
                
                PosReset(1);
                Heart1.SetActive(false);
            }

            if (MaxHP >= 2)
            {
                Heart2.SetActive(true);
            }
            else
            {
                
                PosReset(2);
                Heart2.SetActive(false);
            }

            if (MaxHP >= 3)
            {
                Heart3.SetActive(true);
            }
            else
            {
                PosReset(3);
                PosReset(4);
                Heart3.SetActive(false);
            }

            if (MaxHP >= 4)
            {
                Heart4.SetActive(true);
            }
            else
            {
                PosReset(3);
                PosReset(4);
                Heart4.SetActive(false);
            }


            if (HP < MaxHP)
            {

                if (HP <= 1)
                {
                    DeadHeart1.SetActive(true);
                }
                if (MaxHP == 2)
                {
                    DeadHeart2.SetActive(true);
                }
              
                if (MaxHP == 3)
                {
                    DeadHeart3.SetActive(true);
                }
                if (MaxHP == 4)
                {
                    DeadHeart4.SetActive(true);
                }
            }

            if (HP >=1)
            {
                DeadHeart1.SetActive(false);
            }
            if (HP >= 2)
            {
                DeadHeart2.SetActive(false);
            }
            if (HP >= 3)
            {
                DeadHeart3.SetActive(false);
            }
            if (HP >= 4)
            {
                
                DeadHeart4.SetActive(false);

             
            }

            if (HP> MaxHP)
            {
                if (HP == 3)
                {
                    Heart3.SetActive(true);
                    HPRumble(Heart3);
                    PosReset(3);
                }
                else
                {
                    PosReset(3);
                    // RumbleEnd(Heart3);
                }
                if (HP == 4)
                {
                    Heart4.SetActive(true);
                    HPRumble(Heart4);
                    PosReset(4);
                }
                else
                {
                    PosReset(4);
                    //RumbleEnd(Heart4);
                }
               
            }



        }
    }
}