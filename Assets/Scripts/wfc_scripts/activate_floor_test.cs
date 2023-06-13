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

    //placeholder bools for later loop - jordan
    public bool eSpace = false;
    public bool enEdge = false;
    public bool floor = false;
    public bool stEdge = false;
     
    //-jordan

    GameObject mainLevel;

    void Start() {
        floorList = new List<Transform>();
        int children = transform.childCount;

        //Makes sure all children are hidden at start
        for (int i = 0; i < children; ++i) {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            floorList.Add(child);
            //Debug.Log(string.Format("{0} is in index: {1}", child.gameObject.name, i));
        }
        
        floorList[2].gameObject.SetActive(true); //sets all segment' initial state to 'floor'

        //placeholder bools for later loop - jordan
        eSpace = false;
        enEdge = false;
        floor = true;
        stEdge = false;

        // -jordan

        mainLevel = GameObject.Find("Main Level");
    }



    public int initiate_floor() {
        //hides all children, regadless of child's state
        for (int i = 0; i < floorList.Count; ++i) {
            floorList[i].gameObject.SetActive(false);
        }
        
        //randomly picks floor based on array (which is based on the children in the group)
        randValue = Random.Range(0,floorList.Count);

        // testing something else - jordan maybe put this in a loop that chnages the if statement to cycle between diff bools
        var readMainLevel = mainLevel.GetComponent<TestMill>().readSegmentList[transform.childCount-1].GetChild(0).GetComponent<activate_floor_test>();
        //var readMainLevel = mainLevel.transform.GetChild(transform.childCount-1).GetChild(0).GetComponent<activate_floor_test>(); //1st step

        readProperty = readMainLevel.currentProperty; // 2nd step

        /* if (readMainLevel.eSpace == true) {
            readProperty = 0;
        }

        else if(readMainLevel.enEdge == true) {
            readProperty = 1;
        }

        else if(readMainLevel.floor == true) {
            readProperty = 2;
        }

        else if(readMainLevel.stEdge == true) {
            readProperty = 3;
        } */

        /// tweak this to read the last element value and not itself
        //-jordan

        //main level's read segment list[-1].current
        //Debug.Log("Floor List count is " + floorList.Count);
        for (int i = 0; i < floorList.Count; ++i) {
            //Debug.Log("loopin"); // runs 4 times before no effect and its just 0
            //Debug.Log(i);
            //Debug.Log(floorList[i].gameObject.activeInHierarchy);
             
            /* if (floorList[i].gameObject.activeInHierarchy == true) 
            {
                //thing is set to floorList[i]s property
                //read accecptable list and count

                //i dont think its even running past the if check~!!!!!  -  Jordan :(
                    // something may be wrong with floorlist[i] or llooking for the gameobject
                    //maybe consider assigning values to active children for the outcome: ie  bool eSPace is assigned true when the outcome is empty space ...
                    //...and in this loop do an if check to see if that bool is ture, rather than looking for set axctive because something aint right...
                    //the other bools that would need to be established would then be set to false when a floor option is picked ex: emEdge, floor, stEdge
                    // it may not be what you want to do but we may have to in order to get this working
                        // if eSpace ==  true 
                        // readProperty = value for eSpace
                        //elseif emEdge == true
                        //readProperty = value for emEgde... etc... This would be instead of the "if (floorList[i].gameObject.activeInHierarchy == true)" 


                readProperty = mainLevel.GetComponent<TestMill>().readSegmentList[-1].GetChild(0).GetComponent<activate_floor_test>().floorList[i].GetComponent<wfc_property_test>().myProperty;
                Debug.Log("readprop"+ readProperty);
               
                // readProperty = mainLevel.GetComponent<TestMill>().readSegmentList[-1].GetChild(0).GetComponent<activate_floor_test>().floorList[i].GetComponent<wfc_property_test>().acceptableProperties[Random.Range(0,1)]; // this old one that may be better  ¯\_(ツ)_/¯
               
                //please help
                //need to get the main's last segment's floor group's active floor property
                //please with cherry on top
            }
          */
            //Debug.Log("Read Property is: "+ readProperty);
        }
        
        //int readProperty = mainLevel.transform.GetComponentInChildren<wfc_property_test>().myProperty;

        //Grabs current property
        var readPropertyList = floorList[readProperty].GetComponent<wfc_property_test>(); //3rd step
        int acceptablePropertyCount = readPropertyList.acceptableProperties.Count;
        acceptedProperty = readPropertyList.acceptableProperties[Random.Range(0,acceptablePropertyCount)];
        outProperty = readPropertyList.myProperty;

        //Debug.Log(outProperty);

        //Grabs random element from acceptable properties
        //floorList[acceptableProperty].GetComponent<wfc_property_test>();


        var outcome = floorList[acceptedProperty];
        /// set thingy to make the other bools active or not - jordan
        
        eSpace = false;
        enEdge = false;
        floor = false;
        stEdge = false;
        
        Debug.Log("Accepted Property: " + acceptedProperty);

        /* //just to view the public bools
        if (acceptedProperty == 0) {
            eSpace = true;
        }
        else if (acceptedProperty == 1) {
            enEdge = true;
        }
        else if (acceptedProperty == 2) {
            floor = true;
        }
        else if (acceptedProperty == 3) {
            stEdge = true;
        } */
        // -jordan

        outcome.gameObject.SetActive(true);
        currentProperty = acceptedProperty;
        segmentName = outcome.name;

        return acceptedProperty;
    }
}
