using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_floor_test : MonoBehaviour {

    public List<Transform> floorList;

    private int randValue;
    [HideInInspector] public string segmentName;

    void Start() {
        floorList = new List<Transform>();
        int children = transform.childCount;

        //Makes sure all children are hidden at start
        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            floorList.Add(child);
        }

    }

    public int initiate_floor() {
        for (int i = 0; i < floorList.Count; ++i) {
            floorList[i].gameObject.SetActive(false);
        }
        
        randValue = Random.Range(0,floorList.Count);

        var outcome = floorList[randValue];
        outcome.gameObject.SetActive(true);
        segmentName = outcome.name;

        return randValue;
    }
}
