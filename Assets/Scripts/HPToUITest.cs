using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HeroicArcade.CC.Core
{


    public class HPToUITest : MonoBehaviour
    {
        public GameObject hpTxt;
        private TMP_Text HPDisplay;
        public GameObject playerobj;
        private Player player;
        int HP = 2;
        void Start()
        {
            HPDisplay = hpTxt.GetComponent<TMP_Text>();
            HPDisplay.text = "HP: " + HP.ToString();
           // playerobj = GameObject.Find("Dronion");
            player = playerobj.GetComponent<Player>();
            Debug.Log(player.name);
            
    }


        void Update()
        {
            HP = player.hp;
            HPDisplay.text = "HP: " + HP.ToString();
        }
    }
}