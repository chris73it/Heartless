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
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                barrierList.Add(child);
                //Debug.Log(string.Format("{0} is in index: {1}", child.gameObject.name, i));
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
            
            int myFloorProperty = transform.parent.GetComponentInChildren<final_activate_wfc>().floorProperty;

            int outcome = 0;
            
            var retroSegment = mainLevel.readSegmentList;
            var retro1 = retroSegment[^1].GetComponent<final_activate_wfc>();
            var retro2 = retroSegment[^2].GetComponent<final_activate_wfc>();

            //barrier rules
            //if floor,
            //if retro1 = pitfall {diable barrier}

            bool barrierPass = Random.value <= probability;

            if (barrierPass == true) {
                outcome = 1;

                //only spawns barriers on standard floors
                if (myFloorProperty != 0) {
                    outcome = 0;
                }

                //prevents double barriers on low difficulty
                if (difficulty <= 1 && retro1.barrierProperty != 0) {
                    outcome = 0;
                    checkDebugLog(enableDebugLogs, "Prevented DOUBLE barrier");
                }
                
                // if previous 2 segments has pitfall, no front low barrier
                if (retro1.floorProperty == 1 || retro2.floorProperty == 1) {
                    outcome = 0;
                }
            }

            else {
                outcome = 0;
            }

            barrierList[outcome].gameObject.SetActive(true);
            checkDebugLog(enableDebugLogs, (barrierList[outcome].name + " Activated"));

            return outcome;
        }
    }
}