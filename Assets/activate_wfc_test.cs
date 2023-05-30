using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activate_wfc_test : MonoBehaviour {
    
    public GameObject startEdge;
    public GameObject endEdge;
    public GameObject floor;
    public GameObject empty;

    public List<Transform> list;

    public bool startEdgeVisibility;
    public bool endEdgeVisibility;
    public bool floorVisibility;
    public bool emptyVisibility;

    void Start()
    {
        list = new List<Transform>();
        print("list.Count is " + list.Count);
        int children = transform.childCount;
        Debug.Log("childCount is " + children);
        for (int i = 0; i < children; ++i)
        {
            Transform child = transform.GetChild(i);
            //print("child.name is " + child.name);
            child.gameObject.SetActive(false);
            list.Add(child);
            print("list[" + i +"].gameObject.name is " + list[i].name);
        }
        print("list.Count is " + list.Count);
    }

    void Update() {
        
    }
}
