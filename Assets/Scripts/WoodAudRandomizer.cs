using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{
    public class WoodAudRandomizer : MonoBehaviour
    {
        public AudioClip[] woodsounds;
        private AudioSource suace;
        public GameObject player;


        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Dronion");
            suace = GetComponent<AudioSource>();
            suace.clip = woodsounds[Random.Range(0, woodsounds.Length -1)];
        }



        // Update is called once per frame
        void Update()
        {
            int woodrand = player.GetComponent<Player>().woodrand;

            suace.clip = woodsounds[woodrand];
        }

    }
}
