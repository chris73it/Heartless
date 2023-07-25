using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class HSManager : MonoBehaviour
    {
        private const string HIGH_SCORE_KEY = "HighScore";
        public GameObject playerobj;
        private Player player;
        float score;
        public static int HighScore { get; private set; }
        void Start()
        {
            // playerobj = GameObject.Find("Dronion");
            player = playerobj.GetComponent<Player>();
        }

        private void Awake()
        {
            // Load the high score from PlayerPrefs on game start
            HighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        }
        void Update()
        {
            // Update the distance meter based on player's progress

            score = player.score;
            // Other game logic...

            // When the game ends or the player quits, save the high score
            if (player.death == true)
            {
                HSManager.SaveHighScore((int)score);
            }
        }

        public static void SaveHighScore(int score)
        {
            // Save the high score only if the current score is higher than the saved high score
            if (score > HighScore)
            {
                HighScore = score;
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, HighScore);
                PlayerPrefs.Save();
            }
        }
    }
}
