using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_mill : MonoBehaviour {
        
        [Header("Difficulty")]
        public int difficulty;
        
        [Range(1,5)] //end range = toal segments
        [Tooltip("Selects how many end segments to take into account")]
        public int readRetroSegments;

        [Header("Thresholds")]
        public Vector3 leftMovement;
        public Vector3 leftThreshold;
        public Vector3 rightThreshold;
        //public GameObject[] backgroundPrefabs;
        //public GameObject level;

        [Header("Lists")]
        public List<Transform> segmentList;
        public List<Transform> readSegmentList;

        final_activate_wfc[] activateWFC;

        // hall tweaks
        private float newtime;
        int timecheck;
        
        BackgroundManager bkManager;

        //Creates initial lists
        void Start() {
            newtime = 0;
            timecheck = 15;

            Debug.Log("Current Diffuculty is: " + difficulty);

            bkManager = GameObject.Find("Level").GetComponent<BackgroundManager>();
            leftMovement = bkManager.leftMovement;
            leftThreshold = bkManager.leftThreshold;
            rightThreshold = bkManager.rightThreshold;

            activateWFC = gameObject.GetComponentsInChildren<final_activate_wfc>();
            segmentList = new List<Transform>();

            int children = transform.childCount;

            //Adds each child into segmentList to be read
            for (int i = 0; i < children; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                //child.gameObject.SetActive(false);
                segmentList.Add(child);
                //Debug.Log("Object at index "+i+" in list is "+child.name);
            }

            
            readSegmentList = new List<Transform>(segmentList); //Creates new copied list of segmentList that tracks world order
        }



        //Translates all elements, checks each elements if threshold met
        void FixedUpdate() {
            TimeManagement();

            leftMovement = bkManager.leftMovement;

            transform.position += leftMovement;

            //Cycles elements - world order
            for (int i = 0; i < segmentList.Count; i++) {
                if (segmentList[i].position.z <= leftThreshold.z) {
                    segmentList[i].position += new Vector3(0, 0, rightThreshold.z - leftThreshold.z); //resets segment position

                    var changingSegment = readSegmentList[0]; //remembers element in first position
                    readSegmentList.RemoveAt(0); //removes element in first position
                    activateWFC[i].wfc(); //initiate change
                    readSegmentList.Add(changingSegment);//adds the orignal element to last position
                    //activateWFC[i].wfc(); //initiate change
                }
            }
        }
        
        void TimeManagement() {
            newtime = newtime + (1 * Time.deltaTime);
            //Debug.Log(newtime); 
            // dificulty update checks
            if (newtime > timecheck) {
                difficulty = difficulty + 1;
                timecheck = timecheck + 30;
                Debug.Log(difficulty + " current difficulty increased"); 
            }
        }
    }
}