using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_wfc : MonoBehaviour {

        //public List<Transform> list;
        [HideInInspector] public string segmentName;

        private int randValue;

        final_mill mainLevel;
        final_activate_floor activateFloor;
        //final_activate_obstacles activateObstacle;
        //activate_disruptor_test activateDisruptor;
        final_activate_barrier activateBarrier;
        
        void Start() {
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();
            activateFloor = gameObject.GetComponentInChildren<final_activate_floor>();
            activateBarrier = gameObject.GetComponentInChildren<final_activate_barrier>();
            //activateObstacle = gameObject.GetComponentInChildren<final_activate_obstacles>();
        }

        public void wfc() {
            activateFloor.initiate_floor(mainLevel.difficulty);
            activateBarrier.initiate_barrier(mainLevel.difficulty);
        }
    }
}