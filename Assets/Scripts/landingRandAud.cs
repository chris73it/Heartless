using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{
    public class landingRandAud : MonoBehaviour
    {
        public AudioClip[] soundStore;
        private AudioSource suace;
        public GameObject playerGO;
        
        Player player;


        // Start is called before the first frame update
        void Start()
        {
            playerGO = GameObject.Find("Dronion");
            suace = GetComponent<AudioSource>();
            suace.clip = soundStore[Random.Range(0, soundStore.Length -1)];
            player = playerGO.GetComponent<Player>();
        }



        // Update is called once per frame
        void Update()
        {
            int landrand = player.landrand;

            suace.clip = soundStore[landrand];
        }

       public void LandingPitchRand()
        {
            suace.pitch = Random.Range(0.5f, 1.4f);
        }

    }
}
