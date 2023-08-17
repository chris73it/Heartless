using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_spike : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> spikeList;
        
        public bool enableDebugLogs;

        final_mill mainLevel;

        void Start() {
            spikeList = new List<Transform>();
            
            //0 = empty, 1 = floorSet, 2 = platformSet, 3 = roofSet
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                spikeList.Add(child);
                //Debug.Log("Object at index "+i+" in spikeList is "+child.name);
            }
            
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        void reset_spike_set() {
            //resets all children sets
            for (int i = 0; i < spikeList.Count; ++i) {
                spikeList[i].gameObject.SetActive(false);
            }
        }
        
        int enable_spike_set(int setOutcome) {
            if (setOutcome != 0) {
                //enable spike Set
                spikeList[setOutcome].gameObject.SetActive(true);

                //make tempList of outcome set and disable individual spikes
                var tempSpikeList = new List<Transform>();
                for (int i = 0; i < spikeList[setOutcome].childCount; ++i) {
                    Transform child = spikeList[setOutcome].GetChild(i);
                    child.gameObject.SetActive(false);
                    tempSpikeList.Add(child);
                }

                //generate random value and activate that individual spike
                int tempOutcome = Random.Range(0, tempSpikeList.Count);
                tempSpikeList[tempOutcome].gameObject.SetActive(true);

                return tempOutcome;
            }

            else {
                //add empty spikes to sets?
                return 9;
            }
        }

        public int initiate_spike(int difficulty) {
            reset_spike_set();

            var retro = mainLevel.retroList;
            var selfProperty = transform.parent.GetComponentInChildren<final_activate_wfc>();

            int selfFloorProperty = selfProperty.floorProperty;
            int selfBarrierProperty = selfProperty.barrierProperty;
            int selfPlatformProperty = selfProperty.platformProperty;

            int setOutcome = 0;
            bool spikePass = Random.value <= probability;

            //spike rules
            if (spikePass == true) {
                setOutcome = Random.Range(1,spikeList.Count);
                
                //if no platform, move to floor
                if (selfPlatformProperty == 0) {
                    setOutcome = 1;
                }

                //if spike is floorSet and pitfall = 1, remove pass
                if (setOutcome == 1 && selfFloorProperty == 1) {
                    setOutcome = 0;
                }

                //prevent double spikes
                if (retro[0].spikeProperty != 0) {
                    setOutcome = 0;
                    checkDebugLog(enableDebugLogs, "Prevent DOUBLE Spikes");
                }

                //prevents spikes on barriers
                if (selfBarrierProperty != 0) {
                    setOutcome = 0;
                }
                
                //spikes spawn on 5
                if (difficulty < 5) {
                    setOutcome = 0;
                    checkDebugLog(enableDebugLogs, "Prevented Weak Difficulty");
                }
            }

            int indiv = enable_spike_set(setOutcome);
            checkDebugLog(enableDebugLogs, "Spike Outcome = " + setOutcome + "." + indiv);
            //Debug.Log("Spike Outcome = " + setOutcome);
            return setOutcome;
        }
    }
}