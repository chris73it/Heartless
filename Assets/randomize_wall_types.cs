using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeroicArcade.CC.Core {

    public class randomize_wall_types : MonoBehaviour {

        public List<Transform> wallTypeList;
        public int wallTypeOutcome;
        
        public bool enableDebugLogs;
        
        final_mill mainLevel;

        randomize_barrel_props randomizeProps;
        void Start() {
            wallTypeList = new List<Transform>();
            for (int i = 0; i < transform.childCount; ++i) {
                Transform child = transform.GetChild(i);
                child.gameObject.SetActive(false);
                wallTypeList.Add(child);
                checkDebugLog(enableDebugLogs, (child.gameObject.name + " is in index: " + i));
            }
            wallTypeList[0].gameObject.SetActive(true);
        }

        public void reset_wall() {
            //resets all children
            for (int i = 0; i < wallTypeList.Count; ++i) {
                wallTypeList[i].gameObject.SetActive(false);
            }
        }

        void checkDebugLog(bool check, string stringName) {
            if (check == true) {
                Debug.Log(string.Format(stringName));
            }
        }

        // Update is called once per frame
        public int initiate_wall() {
            reset_wall();
            wallTypeOutcome = Random.Range(0,wallTypeList.Count);
            wallTypeList[wallTypeOutcome].gameObject.SetActive(true);
            return wallTypeOutcome;
        }
    }
}