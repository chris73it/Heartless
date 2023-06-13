using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_mill : MonoBehaviour {
    
    [Range(1,5)] //end range = toal segments
    public int readSegmentsRetro;

    [Header("Thresholds")]
    public Vector3 leftMovement;
    public Vector3 leftThreshold;
    public Vector3 rightThreshold;
    //public GameObject[] backgroundPrefabs;
    //public GameObject level;

    [Header("Lists")]
    public List<Transform> segmentList;
    public List<Transform> readSegmentList;
    public List<int> readSegmentListProperties;

    activate_wfc_test[] activateWFC;

    //Creates initial lists
    void Start() {
        activateWFC = gameObject.GetComponentsInChildren<activate_wfc_test>();
        segmentList = new List<Transform>();
        readSegmentListProperties = new List<int>();

        int children = transform.childCount;

        //Adds each child into segment list to be read
        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            //print("child.name is " + child.name);
            //child.gameObject.SetActive(false);
            segmentList.Add(child);
            //Debug.Log("Object at index "+i+" in list is "+child.name);
        }
        
        readSegmentList = new List<Transform>(segmentList); //Creates new copied list of segmentList that tracks world order

        //set readSegmentListProperties elements
        for (int i = 0; i < readSegmentsRetro; i++) {
            readSegmentListProperties.Add(0);
        }
    }



    //Translates all elements, checks each elements if threshold met
    void FixedUpdate() {
        float thresholdRange = rightThreshold.z - leftThreshold.z;
        transform.position += leftMovement;

        //Cycles elements - world order
        for (int i = 0; i < segmentList.Count; i++) {
            if (segmentList[i].position.z <= leftThreshold.z) {
                segmentList[i].position += new Vector3(0, 0, thresholdRange); //resets segment position

                var changingSegment = readSegmentList[0]; //remembers element in first position
                readSegmentList.RemoveAt(0); //removes element in first position
                activateWFC[i].wfc(); //initiate change
                readSegmentList.Add(changingSegment);//adds the orignal elements to last position
            }
        }

        //write readSegmentList to readSegmentListProperties
        for (int i = 0; i < readSegmentsRetro; i++) {
            readSegmentListProperties[i] = readSegmentList[readSegmentList.Count-1-i].transform.GetChild(0).GetComponent<activate_floor_test>().currentProperty;
        }
    }
}