using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_floor : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> floorList;
        
        public bool enableDebugLogs;

        final_mill mainLevel;

        //Hides all children, populates list with children, sets all tiles to floor
        void Start() {
            floorList = new List<Transform>();
            
            //0 = base, 1 = pitfall
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
                floorList.Add(child);
                checkDebugLog(enableDebugLogs, (child.gameObject.name + " is in index: " + i));
            }
            
            //sets self initial state to 'floor'
            floorList[0].gameObject.SetActive(true);
            
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        public void reset_floor() {
            //resets all children
            for (int i = 0; i < floorList.Count; i++) {
                floorList[i].gameObject.SetActive(false);
            }
        }

        public int initiate_floor(int difficulty) {
            reset_floor();

            var retro = mainLevel.retroList;
            
            int selfPlatformProperty = transform.parent.GetComponentInChildren<final_activate_wfc>().platformProperty;

            int outcome = 0;
            bool pitfallPass = Random.value <= probability;

            //pitfall rules
            if (pitfallPass == true) {
                outcome = 1;
                
                //prevents all pitfalls at difficulty 0
                if (difficulty == 0) {
                    outcome = 0;
                    checkDebugLog(enableDebugLogs, "Prevented SINGLE Pitfall");
                }

                //prevents double pitfalls + creates space at difficulty 1
                if (difficulty <= 1) {
                    if (retro[0].floorProperty == 1 || retro[1].floorProperty == 1) {
                        outcome = 0;
                        checkDebugLog(enableDebugLogs, "Prevented DOUBLE Pitfall and/or created space");
                    }
                }

                //prevents triple pitfall at difficulty >1
                if (difficulty > 1) {
                    if (retro[0].floorProperty == 1 && retro[2].floorProperty == 1) {
                        outcome = 0;
                        checkDebugLog(enableDebugLogs, "Prevented TRIPLE Pitfall");
                    }
                }

                //specific rules
                //if platform then 2 consec pitfalls, PREVENT
                if (retro[2].platformProperty == 1 && retro[1].floorProperty == 1 && retro[0].floorProperty == 1) {
                    outcome = 0;
                    checkDebugLog(enableDebugLogs, "Prevented Platform Cliff");
                }
            }

            floorList[outcome].gameObject.SetActive(true);
            checkDebugLog(enableDebugLogs, (floorList[outcome].name + " Activated"));
            return outcome;
        }
    }
}