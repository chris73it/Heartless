using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wfc_property_test : MonoBehaviour {

    [HideInInspector] public string myTag;
    public int myProperty;
    public List<int> acceptableProperties = new List<int>();

    void Start() {
        myTag = gameObject.tag;
        myProperty = transform.GetSiblingIndex();
    }

    void Update() {
        
    }
}
