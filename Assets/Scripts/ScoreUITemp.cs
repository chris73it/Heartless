using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HeroicArcade.CC.Core
{


    public class ScoreUITemp : MonoBehaviour
    {
        public GameObject scoreTxt;
        private TMP_Text ScoreDisplay;
        public GameObject playerobj;
        private Player player;
        int Score = 0;
        void Start()
        {
            ScoreDisplay = scoreTxt.GetComponent<TMP_Text>();
            ScoreDisplay.text = "Score " + Score.ToString() + "m";
           // playerobj = GameObject.Find("Dronion");
            player = playerobj.GetComponent<Player>();
            Debug.Log(player.name);
            
    }


        void Update()
        {
            Score = player.score;
            ScoreDisplay.text = "Score " + Score.ToString() + "m";
        }
    }
}