using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_floor : MonoBehaviour {

        public int currentProperty;
        public List<Transform> floorList;
        //[SerializeField] GameObject spikeGroup;
        
        private int randValue;
        [HideInInspector] public string segmentName;
       // final_activate_spike activateSpike;

        int acceptedProperty;
        int readProperty;

        final_mill mainLevel;

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
            //activateSpike = spikeGroup.GetComponent<final_activate_spike>(); //gameObject.GetComponentInChildren<final_activate_spike>();
            //if (activateSpike == null) {
             //   Debug.Log("Activate Spike is NULL");
            //}
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }



        public void initiate_floor() {
            //Hides all children
            for (int i = 0; i < floorList.Count; ++i) {
                floorList[i].gameObject.SetActive(false);
            }
            
            randValue = Random.Range(0,floorList.Count); //Randomly picks floor based on array count

            //read 1 segment gernation - start
            var readMainLevel = mainLevel.readSegmentList[transform.childCount-1].GetChild(4).GetComponent<final_activate_floor>(); //1st step - 4th index is PitfallSet
            readProperty = readMainLevel.currentProperty; //2nd step
            
            int startRange = 0;
            
            var readPropertyList = floorList[readProperty].GetComponent<final_wfc_property>(); //3rd step
            int acceptablePropertyCount = readPropertyList.acceptableProperties.Count;
            int acceptedPropertyIndex = Random.Range(startRange,acceptablePropertyCount); //double pitfalls despite no access
            if (startRange == acceptablePropertyCount) {
                acceptedPropertyIndex = 0;
            }

            //if readsegmentretro >1, then use this if statement
            if (mainLevel.readSegmentsRetro > 1) {
                if (mainLevel.readSegmentListProperties[0] == 1 && mainLevel.readSegmentListProperties[1] == 1) { //if 2 floor tiles both equal empty
                    acceptedPropertyIndex = 0;
                    Debug.Log("Double Pitfall Detected");
                }
            }

            //Grabs current property
            acceptedProperty = readPropertyList.acceptableProperties[acceptedPropertyIndex];
            //read 1 segment gernation - end
            


            var outcome = floorList[acceptedProperty];
            //Debug.Log("Accepted Property: " + acceptedProperty);

            outcome.gameObject.SetActive(true);
            currentProperty = acceptedProperty;
            segmentName = outcome.name;

            if (acceptedProperty == 2) {
                //activateSpike.initiate_spike();
            }
        }
    }
}