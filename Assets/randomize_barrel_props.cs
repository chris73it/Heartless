using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class randomize_barrel_props : MonoBehaviour {

        public List<Transform> propGroupList;
        
        public bool enableDebugLogs;

        final_mill mainLevel;

        int propValue;

        void Start() {
            propGroupList = new List<Transform>();
            
            //1 = cabbage, 2 = potato, 3 = tomato, 4 = fish
            for (int i = 1; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                //print("child.name is " + child.name);
                child.gameObject.SetActive(false);
                propGroupList.Add(child);
                //Debug.Log("Object at index "+i+" in propGroupList is "+child.name);
            }
            
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        void reset_prop_group() {
            //resets all children sets
            for (int i = 0; i < propGroupList.Count; ++i) {
                propGroupList[i].gameObject.SetActive(false);
            }
        }

        void random_group() {
            propValue = Random.Range(0, propGroupList.Count-1);
            propGroupList[propValue].gameObject.SetActive(true); // errors point here for some reason 
        }

        public void initiate_randomize_props() {
            reset_prop_group();
            random_group();
            checkDebugLog(enableDebugLogs, "Prop Value = " + propGroupList[propValue].name);
        }
    }
}