using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_floor_test : MonoBehaviour {

    public int currentProperty;
    public List<Transform> floorList;

    private int randValue;
    [HideInInspector] public string segmentName;
    wfc_property_test wfcProperty;

    int outProperty;
    int acceptedProperty;
    int readProperty;

    GameObject mainLevel;
    test_mill testMill;

    //Hides all children, populates list with children, sets all tiles to floor
    void Start() {
        floorList = new List<Transform>();
        int children = transform.childCount;

        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            floorList.Add(child);
            Debug.Log(string.Format("{0} is in index: {1}", child.gameObject.name, i));
        }
        
        floorList[2].gameObject.SetActive(true); //sets all segment' initial state to 'floor'
        
        mainLevel = GameObject.Find("Main Level");
    }



    public int initiate_floor() {
        //Hides all children
        for (int i = 0; i < floorList.Count; ++i) { floorList[i].gameObject.SetActive(false); }
        
        randValue = Random.Range(0,floorList.Count); //Randomly picks floor based on array count


        //read readSegmentListProperties from test_mill
        //implement rules


        //read 1 segment gernation - start
        var readMainLevel = mainLevel.GetComponent<test_mill>().readSegmentList[transform.childCount-1].GetChild(0).GetComponent<activate_floor_test>(); //1st step
        readProperty = readMainLevel.currentProperty; //2nd step

        //Grabs current property
        var readPropertyList = floorList[readProperty].GetComponent<wfc_property_test>(); //3rd step
        int acceptablePropertyCount = readPropertyList.acceptableProperties.Count;
        acceptedProperty = readPropertyList.acceptableProperties[Random.Range(0,acceptablePropertyCount)];
        outProperty = readPropertyList.myProperty;
        //read 1 segment gernation - end

        var outcome = floorList[acceptedProperty];
        //Debug.Log("Accepted Property: " + acceptedProperty);

        outcome.gameObject.SetActive(true);
        currentProperty = acceptedProperty;
        segmentName = outcome.name;

        return acceptedProperty;
    }
}
