using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace HeroicArcade.CC.Core {

    public class final_activate_wfc : MonoBehaviour {

        //public List<Transform> list;
        [HideInInspector] public string segmentName;

        private int randValue;

        final_activate_floor activateFloor;
        //final_activate_obstacles activateObstacle;
        //activate_disruptor_test activateDisruptor;
        
        void Start() {        
            activateFloor = gameObject.GetComponentInChildren<final_activate_floor>();
            //activateObstacle = gameObject.GetComponentInChildren<final_activate_obstacles>();
        }

        public void wfc() {
            activateFloor.initiate_floor();
            //activateObstacle.initiate_obstacle();
        }
    }
}