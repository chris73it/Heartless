using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activate_wfc_test : MonoBehaviour {

    public List<Transform> list;
    public string segmentName;

    private int randValue;

    void Start() {
        list = new List<Transform>();
        int children = transform.childCount;

        //Makes sure all children are hidden at start
        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            //print("child.name is " + child.name);
            child.gameObject.SetActive(false);
            list.Add(child);
            Debug.Log("Object at index "+i+" in list is "+child.name);
        }

        Debug.Log("The list length = " + list.Count);
        randValue = Random.Range(0,list.Count);
        Debug.Log("Random Value = " + randValue);
        list[randValue].gameObject.SetActive(true);
    }

    public void wfc() {
        for (int i = 0; i < list.Count; ++i) {
            list[i].gameObject.SetActive(false);
        }
        randValue = Random.Range(0,list.Count);

        var outcome = list[randValue];
        outcome.gameObject.SetActive(true);     
        segmentName = outcome.name;
    }
}
