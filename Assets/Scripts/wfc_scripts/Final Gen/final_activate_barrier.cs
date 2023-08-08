using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_barrier : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> barrierList;
        
        public bool enableDebugLogs;
        
        final_mill mainLevel;

        //Hides all children, populates list with children
        void Start() {
            barrierList = new List<Transform>();

            //0 = empty, 1 = low front, 2 = high front, 3 = high back
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
                barrierList.Add(child);
                checkDebugLog(enableDebugLogs, (child.gameObject.name + " is in index: " + i));
            }

            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }

        public void reset_barrier() {
            //resets all children
            for (int i = 0; i < barrierList.Count; ++i) {
                barrierList[i].gameObject.SetActive(false);
            }
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        public int initiate_barrier(int difficulty) {
            reset_barrier();

            var retro = mainLevel.retroList;
            
            int selfFloorProperty = transform.parent.GetComponentInChildren<final_activate_wfc>().floorProperty;

            int outcome = 0;
            bool barrierPass = Random.value <= probability;

            //barrier rules
            if (barrierPass == true) {
                outcome = Random.Range(1, barrierList.Count);


                //only spawns barriers on standard floors
                if (selfFloorProperty != 0) {
                    outcome = 0;
                }

                //prevents double barriers + creates space on low difficulty
                if (difficulty <= 1 && (retro[0].barrierProperty != 0 || retro[1].barrierProperty != 0)) {
                    outcome = 0;
                    checkDebugLog(enableDebugLogs, "Prevented DOUBLE barrier and/or created space");
                }
                
                // if previous 2 segments has pitfall, no front low barrier
                if (retro[0].floorProperty == 1 || retro[1].floorProperty == 1) {
                    outcome = 0;
                }
                
                if (outcome != 1) {
                    if (Random.value < .5f) {
                        outcome = 1;
                    }
                }

                //prevent upper barriers on pitfalls
                if ((outcome == 2 || outcome == 3) && selfFloorProperty != 0) {
                    outcome = 1;
                }
            }

            barrierList[outcome].gameObject.SetActive(true);
            checkDebugLog(enableDebugLogs, (barrierList[outcome].name + " Activated"));
            return outcome;
        }
    }
}