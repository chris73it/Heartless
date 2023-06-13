using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMill : MonoBehaviour {
    public Vector3 leftMovement;
    public Vector3 leftThreshold;
    public Vector3 rightThreshold;
    //public GameObject[] backgroundPrefabs;
    //public GameObject level;

    public List<Transform> segmentList;
    public List<Transform> readSegmentList;

    activate_wfc_test[] activateWFC;

    //Creates initial lists
    void Start() {
        activateWFC = gameObject.GetComponentsInChildren<activate_wfc_test>();
        segmentList = new List<Transform>();

        int children = transform.childCount;

        //Adds each child into segment list to be read
        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            //print("child.name is " + child.name);
            //child.gameObject.SetActive(false);
            segmentList.Add(child);
            //Debug.Log("Object at index "+i+" in list is "+child.name);
        }
        
        //Creates new copied list of segmentList that tracks world order
        readSegmentList = new List<Transform>(segmentList);

    }

    //Translates all elements, checks each elements if threshold met
    void FixedUpdate() {
        float thresholdRange = rightThreshold.z - leftThreshold.z;
        transform.position += leftMovement;

        //Cycles elements - world order
        for (int i = 0; i < segmentList.Count; ++i) {
            if (segmentList[i].position.z <= leftThreshold.z) {
                //resets segment position
                segmentList[i].position += new Vector3(0, 0, thresholdRange);

                //remembers element in first position
                var changingSegment = readSegmentList[0];

                //removes element in first position
                readSegmentList.RemoveAt(0);

                //initiate change
                activateWFC[i].wfc();

                //adds the orignal elements to last position
                readSegmentList.Add(changingSegment);
            }
        }
    }
}