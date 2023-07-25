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
        List<final_activate_wfc> retro;

        //Hides all children, populates list with children, sets all tiles to floor
        void Start() {
            floorList = new List<Transform>();
            
            //0 = base, 1 = pitfall
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                floorList.Add(child);
                //Debug.Log(string.Format("{0} is in index: {1}", child.gameObject.name, i));
            }
            
            //sets all segments' initial state to 'floor'
            floorList[0].gameObject.SetActive(true);
            
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>(); //Change to mainLevelRetroSegments?, and move to initiate_floor
            retro = mainLevel.retroList;
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        public int initiate_floor(int difficulty) {

            var retroSegment = mainLevel.readSegmentList;
            var retro1 = retroSegment[^1].GetComponent<final_activate_wfc>();
            var retro2 = retroSegment[^2].GetComponent<final_activate_wfc>();

            //Hides all children
            for (int i = 0; i < floorList.Count; i++) {
                floorList[i].gameObject.SetActive(false);
            }

            int acceptedPropertyIndex;// = Random.Range(0,floorList.Count);
            bool pitfallPass = Random.value <= probability;

            //pitfall rules
            if (pitfallPass == true) {
                acceptedPropertyIndex = 1;
                
                //prevents all pitfalls at difficulty 0
                if (difficulty == 0) {
                    acceptedPropertyIndex = 0;
                    checkDebugLog(enableDebugLogs, "Prevented SINGLE Pitfall");
                }

                //prevents double pitfalls at difficulty 1
                if (difficulty <= 1) {
                    if (retro1.floorProperty == 1) {
                        acceptedPropertyIndex = 0;
                        checkDebugLog(enableDebugLogs, "Prevented DOUBLE Pitfall");
                    }
                }

                //prevents triple pitfall at difficulty >1
                if (difficulty > 1) {
                    if (retro1.floorProperty == 1 && retro2.floorProperty == 1) {
                        acceptedPropertyIndex = 0;
                        checkDebugLog(enableDebugLogs, "Prevented TRIPLE Pitfall");
                    }
                }
                
                //if double or (single + platform)
                //acceptedProperty = 0
            }

            else {
                acceptedPropertyIndex = 0;
            }

            var outcome = floorList[acceptedPropertyIndex];
            //Debug.Log("Outcome = " + outcome.name);
            outcome.gameObject.SetActive(true);

            return acceptedPropertyIndex;
        }
    }
}