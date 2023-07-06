using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_barrier : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> barrierList;

        private int randValue;
        [HideInInspector] public string segmentName;
        
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
        }

        public void reset_barrier() {
            //resets all children
            for (int i = 0; i < barrierList.Count; ++i) {
                barrierList[i].gameObject.SetActive(false);
            }
        }

        public void initiate_barrier() {
            reset_barrier();

            floorProperty = transform.parent.GetComponentInChildren<final_activate_floor>().currentProperty;

            //if tile = floor, proceed
            if (floorProperty == 0) { //if floor property = 0 (floor)
                if (Random.value <= probability) {
                    var outcome = barrierList[2];
                    outcome.gameObject.SetActive(true);
                    //Debug.Log(outcome.name + " Activated");
                }
            }
        }

        //barrier rules
        //if floor, 
        //if pitfall, end barrier = false
        //end barrier rules
    }
}