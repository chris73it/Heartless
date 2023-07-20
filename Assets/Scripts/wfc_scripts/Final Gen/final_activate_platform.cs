using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_platform : MonoBehaviour {
        
        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> platformList;
        
        public bool enableDebugLogs;

        int platformConsec;
        float startProbability;

        final_mill mainLevel;

        //Hides all children, populates list with children
        void Start() {
            platformList = new List<Transform>();

            //0 is empty, 1 is base
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                platformList.Add(child);
                //Debug.Log(string.Format("{0}[{1}] = {2}", platformList, i, child.gameObject.name));
            }

            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();

            platformConsec = 1;
            startProbability = probability;
        }

        public void reset_platform() {
            //resets all children
            for (int i = 0; i < platformList.Count; ++i) {
                platformList[i].gameObject.SetActive(false);
            }
        }

        void myDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        //cap to 4 conseq platforms.
        //platformCount = 0
        //if retro1.platformProperty == 1 platformCount += 1
        //if platformCount == 4 || retro1.platformProperty != 1, output = empty

        public int initiate_platform(int difficulty) {
            //Debug.Log("Platform intantiated.");
            reset_platform();

            int outcome = 0;
            
            var retroSegment = mainLevel.readSegmentList;
            var retro1 = retroSegment[^1].GetComponent<final_activate_wfc>();
            var retro2 = retroSegment[^2].GetComponent<final_activate_wfc>();

            //make as new function???
            ///consecutive property, increase probability every time platform spawns, and reset probability on break
            if (retro1.platformProperty == 1 && platformConsec < 5) {
                platformConsec += 1;
                probability += .1f;
                if (enableDebugLogs == true) {
                    Debug.Log(string.Format("Consequetive Platform: {0}, Probability: {1}", platformConsec, probability));
                }
            }

            else {
                platformConsec = 1;
                probability = startProbability;
                if (enableDebugLogs == true) {
                    Debug.Log(string.Format("RESET Consequetive Platform: {0}, Probability: {1}", platformConsec, probability));
                }
            }

            bool platformPass = Random.value <= probability;

            //platform probability check
            if (platformPass == true) {
                outcome = 1;
                
                //platforms begin on difficulty 2
                if (difficulty < 2) {
                    outcome = 0;
                }

                //platform conseq check
                if (platformConsec >= 4) {
                    outcome = 0;
                }

            }

            //platformCount = mainLevel.readSegmentListProperties[0].GetComponentInChildren<final_activate_platform>().platformCount;

            //if pitfall and platform dont give a shit about upper barriwers 
            //if pitfall and no plat turn of those uppwer fuckers
            //if difficulty == 0 {disable platform}

            //if the platform is instanciating for the first time no start upper barrier

            platformList[outcome].gameObject.SetActive(true);
            return outcome;

        }
    }
}
