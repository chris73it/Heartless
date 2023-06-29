using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activate_wfc_test : MonoBehaviour {

    //public List<Transform> list;
    [HideInInspector] public string segmentName;

    private int randValue;

    activate_floor_test activateFloor;
    activate_obstacle_test activateObstacle;
    //activate_disruptor_test activateDisruptor;
    
    void Start() {        
        activateFloor = gameObject.GetComponentInChildren<activate_floor_test>();
        activateObstacle = gameObject.GetComponentInChildren<activate_obstacle_test>();
    }

    public void wfc() {
        activateFloor.initiate_floor();
        activateObstacle.initiate_obstacle();
    }
}
