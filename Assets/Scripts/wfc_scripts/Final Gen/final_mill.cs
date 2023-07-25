using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_mill : MonoBehaviour {
        
        [Header("Difficulty")]
        [Tooltip("Sets starting Difficulty")]
        public int difficulty;
        
        [Range(1,5)] //end range = total segments
        [Tooltip("Selects how many end segments to take into account")]
        public int retroSegmentCount;

        [Header("Thresholds")]
        public Vector3 leftMovement;
        public Vector3 leftThreshold;
        public Vector3 rightThreshold;
        //public GameObject[] backgroundPrefabs;
        //public GameObject level;

        [Header("Lists")]
        [Tooltip("List of all segments under the manager")]
        public List<Transform> segmentList;
        [Tooltip("List of all segments cycling under the manager")]
        public List<Transform> readSegmentList;
        [Tooltip("List of properties of all segments cycling under the manager")]
        public List<final_activate_wfc> retroList;

        [Header("Debug")]
        public bool listsDebugLog;
        public bool difficultyDebugLog;

        final_activate_wfc[] activateWFC;

        // hall tweaks
        private float newtime;
        int timecheck;
        
        BackgroundManager bkManager;

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        void retroListCreation() {
            retroList = new List<final_activate_wfc>();
            for (int i = 0; i < 10; ++i) {
                retroList.Add(readSegmentList[^(1+i)].GetComponent<final_activate_wfc>());
                //Debug.Log(retroList[i].name);
            }
            Debug.Log("Recentest Floor Property: " + retroList[0].floorProperty);
        }

        //Creates initial lists
        void Start() {
            newtime = 0;
            timecheck = 15;

            checkDebugLog(difficultyDebugLog, ("Current Difficulty: " + difficulty));

            bkManager = GameObject.Find("Level").GetComponent<BackgroundManager>();
            //leftMovement = bkManager.leftMovement;
            leftThreshold = bkManager.leftThreshold;
            rightThreshold = bkManager.rightThreshold;

            activateWFC = gameObject.GetComponentsInChildren<final_activate_wfc>();
            segmentList = new List<Transform>();

            //Adds each child into segmentList to be read
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                segmentList.Add(child);

                checkDebugLog(listsDebugLog, (child.name + " at index: [" + i + "]"));
            }
            
            readSegmentList = new List<Transform>(segmentList); //Creates new copied list of segmentList that tracks world order
            //retroListCreation();
        }

        //Translates all elements, checks each elements if threshold met
        void FixedUpdate() {
            TimeManagement();

            leftMovement = bkManager.leftMovement;
            transform.position += leftMovement;

            //Cycles elements - world order
            //Checks each element
            for (int i = 0; i < segmentList.Count; i++) {
                //Detects change in element
                if (segmentList[i].position.z <= leftThreshold.z) {
                    segmentList[i].position += new Vector3(0, 0, rightThreshold.z - leftThreshold.z); //resets segment position

                    var changingSegment = readSegmentList[0]; //remembers element in first position
                    readSegmentList.RemoveAt(0); //removes element in first position
                    activateWFC[i].wfc(); //initiate change, must occur before adding back to list
                    readSegmentList.Add(changingSegment);//adds orignal element to last position

                    checkDebugLog(listsDebugLog, (changingSegment.name + " Reset"));
                    
                    //retroListCreation();
                }
            }
        }
        
        void TimeManagement() {
            newtime = newtime + (1 * Time.deltaTime);
            //Debug.Log(newtime); 
            //dificulty update checks
            if (newtime > timecheck) {
                difficulty = difficulty + 1;
                timecheck = timecheck + 30;

                checkDebugLog(difficultyDebugLog, ("Increased Difficulty: " + difficulty));
            }
        }
    }
}