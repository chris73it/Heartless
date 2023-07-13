using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_platform : MonoBehaviour {
        
        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;

        public int currentProperty; //0 is empty, 1 is base
        public List<Transform> platformList;

        [HideInInspector] public int platformCount;

        final_mill mainLevel;

        void Start() {
            platformList = new List<Transform>();
            int children = transform.childCount;

            for (int i = 0; i < children; ++i) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
                platformList.Add(child);
                //Debug.Log(child);
            }

            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }

        public void reset_platform() {
            //resets all children
            for (int i = 0; i < platformList.Count; ++i) {
                platformList[i].gameObject.SetActive(false);
            }
        }


        //cap to 4 conseq platforms.
        //platformCount = 0
        //on initiate platformCount += 1
        //if platformCount == 4, output = empty

        public int initiate_platform(int difficulty) {
            //Debug.Log("Platform intantiated.");
            reset_platform();

            int outcome = 0;

            //platformCount = mainLevel.readSegmentListProperties[0].GetComponentInChildren<final_activate_platform>().platformCount;

            // if pitfall and platform dont give a shit about upper barriwers 
            //if pitfall and no plat turn of those uppwer fuckers 

            //if the platform is instanciating for the first time no start upper barrier

            

            if (Random.value <= probability) {
                outcome = 1;
                platformCount += 1;
                //Debug.Log("Platform Count: " + platformCount);
            }

            platformList[outcome].gameObject.SetActive(true);
            currentProperty = outcome;

            return outcome;

        }
    }
}
