using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core {

    public class final_activate_wfc : MonoBehaviour {

        final_mill mainLevel;
        final_activate_floor activateFloor;
        final_activate_barrier activateBarrier;
        final_activate_platform activatePlatform;

        public int floorProperty;
        public int barrierProperty;
        public int platformProperty;
        
        void Start() {
            mainLevel = GameObject.Find("Final BK Manager").GetComponent<final_mill>();

            activateFloor = gameObject.GetComponentInChildren<final_activate_floor>();
            activateBarrier = gameObject.GetComponentInChildren<final_activate_barrier>();
            activatePlatform = gameObject.GetComponentInChildren<final_activate_platform>();
        }

        public void wfc() {
            floorProperty = activateFloor.initiate_floor(mainLevel.difficulty);
            barrierProperty = activateBarrier.initiate_barrier(mainLevel.difficulty);
            platformProperty = activatePlatform.initiate_platform(mainLevel.difficulty);

            //Debug.Log(string.Format("{0} has Floor: {1}, Barrier: {2}, Platform: {3}", transform.name, floorProperty, barrierProperty, platformProperty));
        }
    }
}