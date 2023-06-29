using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_floor_test : MonoBehaviour {

    public int currentProperty;
    public List<Transform> floorList;
    [SerializeField] GameObject spikeGroup;
    
    private int randValue;
    [HideInInspector] public string segmentName;
    activate_spike_test activateSpike;

    int acceptedProperty;
    int readProperty;

    test_mill mainLevel;

    //Hides all children, populates list with children, sets all tiles to floor
    void Start() {
        floorList = new List<Transform>();
        int children = transform.childCount;

        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            floorList.Add(child);
            //Debug.Log(string.Format("{0} is in index: {1}", child.gameObject.name, i));
        }
        
        floorList[0].gameObject.SetActive(true); //sets all segment' initial state to 'floor'
        activateSpike = spikeGroup.GetComponent<activate_spike_test>(); //gameObject.GetComponentInChildren<activate_spike_test>();
        if (activateSpike == null) {
            Debug.Log("Activate Spike is NULL");
        }
        mainLevel = GameObject.Find("Main Level").GetComponent<test_mill>();
    }



    public void initiate_floor() {
        //Hides all children
        for (int i = 0; i < floorList.Count; ++i) {
            floorList[i].gameObject.SetActive(false);
        }
        
        randValue = Random.Range(0,floorList.Count); //Randomly picks floor based on array count

        //read 1 segment gernation - start
        var readMainLevel = mainLevel.readSegmentList[transform.childCount-1].GetChild(0).GetComponent<activate_floor_test>(); //1st step
        readProperty = readMainLevel.currentProperty; //2nd step
        
        int startRange = 0;

        if (mainLevel.readSegmentListProperties[0] == 0 && mainLevel.readSegmentListProperties[1] == 0) { //if 2 floor tiles both equal empty
            startRange += 1;
            Debug.Log("Double Empty Detected");
        }

        //Grabs current property
        var readPropertyList = floorList[readProperty].GetComponent<wfc_property_test>(); //3rd step
        int acceptablePropertyCount = readPropertyList.acceptableProperties.Count;
        acceptedProperty = readPropertyList.acceptableProperties[Random.Range(startRange,acceptablePropertyCount)];
        //read 1 segment gernation - end
        


        var outcome = floorList[acceptedProperty];
        //Debug.Log("Accepted Property: " + acceptedProperty);

        outcome.gameObject.SetActive(true);
        currentProperty = acceptedProperty;
        segmentName = outcome.name;

        if (acceptedProperty == 2) {
            activateSpike.initiate_spike();
        }
    }
}
