using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_barrier : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> barrierList;

        private int randValue; // used for a 1 in 5 
        [HideInInspector] public string segmentName;
        
        final_mill mainLevel;

        int floorProperty;
        
        void Start() {
            barrierList = new List<Transform>();
            //Makes sure all children are hidden at start
            //Debug.Log(transform.childCount);
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                barrierList.Add(child);
                //Debug.Log("Object at index "+i+" in barrierList is "+child.name);
            }

            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }

        public void reset_barrier() {
            //resets all children
            for (int i = 0; i < barrierList.Count; ++i) {
                barrierList[i].gameObject.SetActive(false);
            }
        }

        public void initiate_barrier(int difficulty) {
            reset_barrier();
            
            floorProperty = transform.parent.GetComponentInChildren<final_activate_floor>().currentProperty;

            int outcome = 0;

            if (Random.value <= probability) {//if pass probability, continue 
                if (floorProperty == 0) { //if floor property = 0 (floor)
                    outcome = 3;
                }
                
                else if (difficulty >= 2 && floorProperty != 0) {
                    if (Random.Range(1,5) == 1) {
                    outcome = 3; // Front Low Barrier
                    }
                }
                
                else if (mainLevel.readSegmentListProperties[0] == 1 || mainLevel.readSegmentListProperties[1] == 1) { // if previous segment has pitfall, no front low barrier
                    outcome = 0;
                }

                else {
                    outcome = 0;
                }
            }

            barrierList[outcome].gameObject.SetActive(true);
            //Debug.Log(barrierList[outcome].name + " Activated");

        }

        //barrier rules
        //if floor, 
        //if pitfall, end barrier = false
        //end barrier rules
    }
}