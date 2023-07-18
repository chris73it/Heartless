using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_floor : MonoBehaviour {

        [Range(0.0f, 1.0f)]
        public float probability = 0.5f;
        public List<Transform> floorList;
        
        [HideInInspector] public string segmentName;

        final_mill mainLevel;

        //Hides all children, populates list with children, sets all tiles to floor
        void Start() {
            floorList = new List<Transform>();
            
            //0 = base, 1 = pitfall
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
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
            int readPropertyValue = mainLevel.readSegmentList[^1].GetComponent<final_activate_wfc>().floorProperty; //1st step - read property of last segment
            
            var readPropertyList = floorList[readPropertyValue].GetComponent<final_wfc_property>(); //2nd step
            //Debug.Log("Previous Floor: " + readPropertyList);

            int acceptedPropertyIndex;// = Random.Range(0,floorList.Count);

            if (probability <= Random.value) {
                acceptedPropertyIndex = 1;
            }

            else {
                acceptedPropertyIndex = 0;
            }

            //pitfall rules
            if (acceptedPropertyIndex == 1) {
                //prevents all pitfalls at difficulty 0
                if (difficulty == 0) {
                    acceptedPropertyIndex = 0;
                    Debug.Log("Prevented SINGLE Pitfall");
                }

                //prevents double pitfalls at difficulty 1
                if (difficulty <= 1) {
                    if (retro1.floorProperty == 1) {
                        acceptedPropertyIndex = 0;
                        Debug.Log("Prevented DOUBLE Pitfall");
                    }
                }

                //prevents triple pitfall at difficulty >1
                if (difficulty > 1) {
                    if (retro1.floorProperty == 1 && retro2.floorProperty == 1) {
                        acceptedPropertyIndex = 0; //set to floorBase
                        Debug.Log("Prevented TRIPLE Pitfall");
                    }
                }
                
                //if double or (single + platform)
                //acceptedProperty = 0
            }

            else {
                acceptedPropertyIndex = 0;
            }

            var outcome = floorList[acceptedPropertyIndex];
            //Debug.Log("Outcome = " + outcome.name);
            outcome.gameObject.SetActive(true);

            return acceptedPropertyIndex;
        }
    }
}