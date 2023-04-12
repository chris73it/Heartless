using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour {
    Vector3 leftMovement;
    Vector3 leftThreshold;
    Vector3 rightThreshold;
    public GameObject[] backgroundPrefabs;
    GameObject level;
    Quaternion rotation;

    void Start() {
        rotation = Quaternion.Euler(0, 90, 0);

        level = GameObject.Find("Level");
        var backgroundManager = level.GetComponent<BackgroundManager>();
        leftMovement = backgroundManager.leftMovement;
        leftThreshold = backgroundManager.leftThreshold;
        rightThreshold = backgroundManager.rightThreshold;
        backgroundPrefabs = backgroundManager.backgroundPrefabs;
    }

    void Update() {
        transform.position += leftMovement * Time.deltaTime;

        if (transform.position.z < leftThreshold.z) {
            int index = Random.Range(0, backgroundPrefabs.Length);

            var instance = Instantiate(backgroundPrefabs[index], rightThreshold, rotation);
            instance.transform.parent = level.transform;

            Destroy(gameObject);
        }
    }
}
