using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_floor : MonoBehaviour {

        public int currentProperty; // 0 = base, 1 = pitfall (randomize pitfalls TBD)
        public List<Transform> floorList;
        //[SerializeField] GameObject spikeGroup;
        
        [HideInInspector] public string segmentName;
       // final_activate_spike activateSpike;

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
            /* activateSpike = spikeGroup.GetComponent<final_activate_spike>(); //gameObject.GetComponentInChildren<final_activate_spike>();
            if (activateSpike == null) {
                Debug.Log("Activate Spike is NULL");
            } */
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>(); //Change to mainLevelRetroSegments?, and move to initiate_floor
        }



        public int initiate_floor(int difficulty) {
            //Hides all children

            var retroSegment = mainLevel.readSegmentList;
            var retro1 = retroSegment[^1].GetComponent<final_activate_wfc>();
            var retro2 = retroSegment[^2].GetComponent<final_activate_wfc>();

            for (int i = 0; i < floorList.Count; ++i) {
                floorList[i].gameObject.SetActive(false);
            }

            //read 1 segment gernation - start
            int readPropertyValue = mainLevel.readSegmentList[^1].Find("PitfallSet").GetComponent<final_activate_floor>().currentProperty; //1st step - read property of last segment
            int startRange = 0;
            
            var readPropertyList = floorList[readPropertyValue].GetComponent<final_wfc_property>(); //2nd step
            //Debug.Log("Previous Floor: " + readPropertyList);

            int acceptablePropertyCount = readPropertyList.acceptableProperties.Count;
            int acceptedPropertyIndex = Random.Range(startRange,acceptablePropertyCount);
            //int acceptedPropertyIndex = 0;

            //Prevents issues for Random.Range
            if (startRange == acceptablePropertyCount) {
                acceptedPropertyIndex = 0;
            }

            //pitfall rules

                //if difficulty = 0, always base
                if (difficulty == 0) {
                    acceptedPropertyIndex = 0;
                }

                //if retro = 1, limit to 1 conseq pitfall

                //mainLevel.readSegementsRetro -> mainLevelRetroSegments[]?, 
                if (mainLevel.readRetroSegments == 1) {
                    if (retroSegment[^1].GetComponent<final_activate_wfc>().floorProperty == 1) {
                        acceptedPropertyIndex = 0;
                    }
                }

                //if diffuclty = 1 and 
                if (retro1.floorProperty == 1 && difficulty == 1) { //reads most recent index to prevent double pitfalls
                    acceptedPropertyIndex = 0; //set to floorBase
                    Debug.Log("Prevented Double Pitfall");
                }

                //if readsegmentretro >1, then use this if statement
                if (mainLevel.readRetroSegments > 1 && difficulty > 1) { //if retro > 1, then prevent triple pitfalls
                    if (retro1.floorProperty == 1 && retro2.floorProperty == 1) {
                        acceptedPropertyIndex = 0;
                        Debug.Log("Prevented Triple Pitfall");
                    }
                }
            //end pitfall rules

            //Grabs current property
            int acceptedProperty = readPropertyList.acceptableProperties[acceptedPropertyIndex];
            //read 1 segment gernation - end
            

            var outcome = floorList[acceptedProperty];
            //Debug.Log("Accepted Property: " + acceptedProperty);

            outcome.gameObject.SetActive(true);
            currentProperty = acceptedProperty;
            segmentName = outcome.name;

            return acceptedProperty;

            //return acceptedProperty;
        }
    }
}