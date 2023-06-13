using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject ballPrefab;
    public Transform spawnPoint;
    
    GameObject ball;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnBall(spawnPoint.position);
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);

            SpawnBall(spawnPos);
        }
    }

    void SpawnBall(Vector3 pos) {
        ball = Instantiate(ballPrefab, pos, Quaternion.identity);
    }
}
