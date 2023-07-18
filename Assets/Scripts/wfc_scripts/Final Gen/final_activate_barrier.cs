using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_barrier : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> barrierList;

        [HideInInspector] public string segmentName;
        
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

        public int initiate_barrier(int difficulty) {
            reset_barrier();
            
            int myFloorProperty = transform.parent.GetComponentInChildren<final_activate_wfc>().floorProperty;

            int outcome = 0;
            
            var retroSegment = mainLevel.readSegmentList;
            var retro1 = retroSegment[^1].GetComponent<final_activate_wfc>();
            //var retro1 = retroSegment[^1].floorProperty;
            var retro2 = retroSegment[^2].GetComponent<final_activate_wfc>();

            //if pass probability, continue
            if (Random.value <= probability) {

                // if floor = 0 (standard), then otucome is front low
                if (myFloorProperty == 0) {
                    outcome = 1;
                }
                
                //chance for front low barrier if pitfall
                else if (difficulty >= 2 && myFloorProperty != 0) {
                    // 20%
                    if (Random.Range(1,5) == 1) {
                    outcome = 1;
                    }
                }
                
                // if previous segment has pitfall, no front low barrier //mainLevel.readSegmentListProperties[] -> mainLevelRetroSegments[].floorProperty
                else if (retro1.floorProperty == 1 || retro2.floorProperty == 1) {
                    outcome = 0;
                }

                // if no rule, no barrier
                else {
                    outcome = 0;
                }
            }

            else {
                outcome = 0;
            }

            barrierList[outcome].gameObject.SetActive(true);
            //Debug.Log(barrierList[outcome].name + " Activated");

            return outcome;

        }

        //barrier rules
        //if floor, 
        //if pitfall, end barrier = false
        //end barrier rules
    }
}