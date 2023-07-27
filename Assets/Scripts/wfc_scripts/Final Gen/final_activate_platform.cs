using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_platform : MonoBehaviour {
        
        [Range(0.0f, 1.0f)]
        public float probability;
        [Range(1, 10)]
        public int consecutivePlatforms;
        public List<Transform> platformList;
        
        public bool enableDebugLogs;

        int platformCounter;
        float startProbability;

        final_mill mainLevel;

        //Hides all children, populates list with children
        void Start() {
            platformList = new List<Transform>();

            //0 is empty, 1 is base
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
                platformList.Add(child);
                checkDebugLog(enableDebugLogs, (child.gameObject.name + " is in index: " + i));
            }

            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();

            platformCounter = mainLevel.platformCounter;
            startProbability = probability;
        }

        public void reset_platform() {
            //resets all children
            for (int i = 0; i < platformList.Count; ++i) {
                platformList[i].gameObject.SetActive(false);
            }
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        void consecutiveFactor(ref int counter, ref int outcomeCheck) {
            if (counter <= consecutivePlatforms && outcomeCheck == 1) {
                counter += 1;
                probability += .1f;
                checkDebugLog(enableDebugLogs, ("Platform Counter: " + counter + ", Probability: " + probability));
            }

            else {
                counter = 0;
                outcomeCheck = 0;
                probability = startProbability;
                checkDebugLog(enableDebugLogs, ("RESET Platform Counter"));
            }
        }

        public int initiate_platform(int difficulty) {
            reset_platform();
            
            var retro = mainLevel.retroList;

            int outcome = 0;

            bool platformPass = Random.value <= probability;

            //platform probability check
            if (platformPass == true) {
                outcome = 1;
                
                //platforms begin on difficulty 2
                if (difficulty < 2) {
                    outcome = 0;
                }
                
                consecutiveFactor(ref platformCounter, ref outcome);

                //always leaves 2 spaces on break
                if (retro[1].platformProperty == 1 && retro[0].platformProperty == 0) {
                    outcome = 0;
                }
            }

            else {
                outcome = 0;
            }

            //if pitfall and platform dont give a shit about upper barriwers 
            //if pitfall and no plat turn of those uppwer fuckers
            //if difficulty == 0 {disable platform}

            platformList[outcome].gameObject.SetActive(true);
            checkDebugLog(enableDebugLogs, (platformList[outcome].name + " Activated"));
            return outcome;
        }
    }
}
