using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_floor_test : MonoBehaviour {

    public List<Transform> floorList;

    private int randValue;
    [HideInInspector] public string segmentName;
    wfc_property_test wfcProperty;

    int currentProperty;
    int acceptableProperty;

    void Start() {
        floorList = new List<Transform>();
        int children = transform.childCount;

        //Makes sure all children are hidden at start
        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            floorList.Add(child);
            Debug.Log(string.Format("{0} is in index: {1}", child.gameObject.name, i));
        }
    }

    public int initiate_floor() {
        //hides all children, regadless of child's state
        for (int i = 0; i < floorList.Count; ++i) {
            floorList[i].gameObject.SetActive(false);
        }
        
        //randomly picks floor based on array (which is based on the children in the group)
        randValue = Random.Range(0,floorList.Count);

        var outcome = floorList[randValue];
        outcome.gameObject.SetActive(true);
        segmentName = outcome.name;

        //Grabs current property
        currentProperty = floorList[2].GetComponent<wfc_property_test>().myProperty;
        //Grabs random element from acceptable properties
        acceptableProperty = Random.Range(1,floorList[2].GetComponent<wfc_property_test>().acceptableProperties.Count);
        //floorList[acceptableProperty].GetComponent<wfc_property_test>();

        return acceptableProperty;
    }
}
