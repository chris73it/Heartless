using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_spike : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> spikeList;

        private int randValue;
        [HideInInspector] public string segmentName;

        void Start() {
            spikeList = new List<Transform>();

            //Makes sure all children are hidden at start
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                spikeList.Add(child);
                //Debug.Log("Object at index "+i+" in spikeList is "+child.name);
            }
        }

        public void reset_spike() {
            //resets all children
            for (int i = 0; i < spikeList.Count; ++i) {
                spikeList[i].gameObject.SetActive(false);
            }
        }

        public void initiate_spike() {
            reset_spike();
            if (Random.value <= probability) {
                var outcome = spikeList[Random.Range(0,spikeList.Count)];
                outcome.gameObject.SetActive(true);
                //Debug.Log(outcome.name + " Activated");
            }
        }
    }
}