using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class TestMill : MonoBehaviour {
        public Vector3 leftMovement;
        public Vector3 leftThreshold;
        public Vector3 rightThreshold;
        //public GameObject[] backgroundPrefabs;
        //public GameObject level;
        Quaternion rotation;

        public List<Transform> segmentList;
        public List<Transform> readSegmentList;

        activate_wfc_test[] activateWFC;

        void Start() {
            activateWFC = gameObject.GetComponentsInChildren<activate_wfc_test>();
            
            segmentList = new List<Transform>();

            int children = transform.childCount;

            for (int i = 0; i < children; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                //child.gameObject.SetActive(false);
                segmentList.Add(child);
                //Debug.Log("Object at index "+i+" in list is "+child.name);
            }
            //creates new list that references the elements of the referenced list
            readSegmentList = new List<Transform>(segmentList);
        }

        void FixedUpdate() {
            float thresholdRange = rightThreshold.z - leftThreshold.z;
            transform.position += leftMovement;
            for (int i = 0; i < segmentList.Count; ++i) {
                //list[i].position += leftMovement;
                if (segmentList[i].position.z <= leftThreshold.z) {
                    //resets segment
                    segmentList[i].position += new Vector3(0, 0, thresholdRange);

                    //remembers element in first position
                    var changingSegment = readSegmentList[0];
                    //removes element in first position
                    readSegmentList.RemoveAt(0);
                    activateWFC[i].wfc();
                    //adds the orignal elements to last position
                    readSegmentList.Add(changingSegment);
                }
            }
            
        }
    }
}
