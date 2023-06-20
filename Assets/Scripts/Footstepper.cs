using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{
    public class Footstepper : MonoBehaviour
    {
        public AudioClip[] stepsounds;
        private AudioSource suace;
        public GameObject playerGO;
        
        Player player;


        // Start is called before the first frame update
        void Start()
        {
            playerGO = GameObject.Find("Dronion");
            suace = GetComponent<AudioSource>();
            suace.clip = stepsounds[Random.Range(0, stepsounds.Length -1)];
            player = playerGO.GetComponent<Player>();
        }



        // Update is called once per frame
        void Update()
        {
            int Steprand = player.steprand;

            suace.clip = stepsounds[Steprand];
        }

       public void PitchRand()
        {
            suace.pitch = Random.Range(0.5f, 1.1f);
        }

    }
}
