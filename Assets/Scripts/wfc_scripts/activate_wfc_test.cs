using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class activate_wfc_test : MonoBehaviour {

    //public List<Transform> list;
    [HideInInspector] public string segmentName;

    private int randValue;

    activate_floor_test activateFloor;
    activate_obstacle_test activateObstacle;
    activate_spike_test activateSpike;
    //activate_disruptor_test activateDisruptor;
    
    void Start() {        
        activateFloor = gameObject.GetComponentInChildren<activate_floor_test>();
        activateObstacle = gameObject.GetComponentInChildren<activate_obstacle_test>();
        activateSpike = gameObject.GetComponentInChildren<activate_spike_test>();
    }

    public void wfc() {
        activateSpike.reset_spike();
        int floor_index = activateFloor.initiate_floor();
        if (floor_index == 2 && (Random.value < activateSpike.probability)) {
            activateSpike.initiate_spike();
        }
        activateObstacle.initiate_obstacle();
    }
}
