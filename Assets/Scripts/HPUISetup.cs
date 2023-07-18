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


        public GameObject playerobj;
        private Player player;
        int MaxHP = 2;
        int HP = 2;
        void Start()
        {
            
            // playerobj = GameObject.Find("Dronion");
            player = playerobj.GetComponent<Player>();
            Debug.Log(player.name);

        }

        // Update is called once per frame
        void Update()
        {
            HP = player.hp;
            MaxHP = player.maxHP;

            if (MaxHP >= 1)
            {
                Heart1.SetActive(true);
            }
            else
            {
                Heart1.SetActive(false);
            }

            if (MaxHP >= 2)
            {
                Heart2.SetActive(true);
            }
            else
            {
                Heart2.SetActive(false);
            }

            if (MaxHP >= 3)
            {
                Heart3.SetActive(true);
            }
            else
            {
                Heart3.SetActive(false);
            }

            if (MaxHP >= 4)
            {
                Heart4.SetActive(true);
            }
            else
            {
                Heart4.SetActive(false);
            }


            if (HP < MaxHP)
            {
                if (MaxHP == 4)
                {
                    DeadHeart4.SetActive(true);
                }
                if (MaxHP == 3)
                {
                    DeadHeart3.SetActive(true);
                }
                if (MaxHP == 2)
                {
                    DeadHeart2.SetActive(true);
                }
                if (HP <=1)
                {
                    DeadHeart1.SetActive(true);
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
                }
                if (HP == 4)
                {
                    Heart4.SetActive(true);
                }
               
            }


        }
    }
}