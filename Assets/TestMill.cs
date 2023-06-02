using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core
{


    public class TestMill : MonoBehaviour
    {
        Vector3 leftMovement;
        Vector3 leftThreshold;
        Vector3 rightThreshold;
        //public GameObject[] backgroundPrefabs;
        public GameObject level;
        Quaternion rotation;

        activate_wfc_test activateWFC;

        void Start()
        {
            var backgroundManager = level.GetComponent<BackgroundManager>();
            leftMovement = backgroundManager.leftMovement;
            leftThreshold = backgroundManager.leftThreshold;
            rightThreshold = backgroundManager.rightThreshold;
            activateWFC = transform.GetComponent<activate_wfc_test>();
            //backgroundPrefabs = backgroundManager.backgroundPrefabs;
        }

        void FixedUpdate()
        {
            transform.position += leftMovement;
            float thresholdRange = rightThreshold.z - leftThreshold.z;
            if (transform.position.z <= leftThreshold.z)
            {
                transform.position += new Vector3(0, 0, thresholdRange);
                activateWFC.wfc();
                Debug.Log(transform.name + " is now " + activateWFC.segmentName);
                // the vector 3 with the value of 40 moves the platforms instead of teleporting them in order to plug gaps
            }
        }
    }
}
