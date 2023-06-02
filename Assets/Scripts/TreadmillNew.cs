using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class TreadmillNew : MonoBehaviour
    {
        Vector3 leftMovement;
        Vector3 leftThreshold;
        Vector3 rightThreshold;
        //public GameObject[] backgroundPrefabs;
        GameObject level;
        Quaternion rotation;

        void Start()
        {
            // rotation = Quaternion.Euler(0, 90, 0);

            level = GameObject.Find("Level");
            var backgroundManager = level.GetComponent<BackgroundManager>();
            leftMovement = backgroundManager.leftMovement;
            leftThreshold = backgroundManager.leftThreshold;
            rightThreshold = backgroundManager.rightThreshold;
            //backgroundPrefabs = backgroundManager.backgroundPrefabs;
        }

        void FixedUpdate()
        {//????????? update/ fixedupdate
            var backgroundManager = level.GetComponent<BackgroundManager>();
            leftMovement = backgroundManager.leftMovement;


            transform.position += leftMovement;// * Time.deltaTime;//?????????

            if (transform.position.z <= leftThreshold.z)
            {
                //int index = Random.Range(0, backgroundPrefabs.Length);



                transform.position = transform.position + new Vector3(0, 0, 40);//rightThreshold;
                                                                                // the vector 3 with the value of 40 moves the platforms instead of teleporting them in order to plug gaps
            }
        }
    }
}