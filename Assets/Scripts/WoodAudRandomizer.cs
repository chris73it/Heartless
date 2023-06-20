using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{
    public class WoodAudRandomizer : MonoBehaviour
    {
        public AudioClip[] woodsounds;
        private AudioSource suace;
        public GameObject playerGO;
        
        Player player;


        // Start is called before the first frame update
        void Start()
        {
            playerGO = GameObject.Find("Dronion");
            suace = GetComponent<AudioSource>();
            suace.clip = woodsounds[Random.Range(0, woodsounds.Length -1)];
            player = playerGO.GetComponent<Player>();
        }



        // Update is called once per frame
        void Update()
        {
            int woodrand = player.woodrand;

            suace.clip = woodsounds[woodrand];
        }

       public void PitchRand()
        {
            suace.pitch = Random.Range(0.5f, 1.1f);
        }

    }
}
