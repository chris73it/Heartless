using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    [SerializeField] float colorDuration;
    [SerializeField] Color collisionColor;
    Color finalColor;
    Color initialColor;

    // Initializes initial color
    void Start() {
        finalColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    // void Update() {
    // }

    private void OnCollisionEnter(Collision collision) {
        StartCoroutine("ChangeColorForOneSecond", colorDuration);
    }

    IEnumerator ChangeColorForOneSecond(float colorDuration) {
        initialColor = GetComponent<Renderer>().material.color;
 
        for (float timer = 1f; timer >= 0; timer -= (1.0f/colorDuration)*Time.deltaTime) {
            Color timerColor = Color.Lerp(initialColor, collisionColor, timer);

            GetComponent<Renderer>().material.color = timerColor;

            yield return null;
        }
        
        GetComponent<Renderer>().material.color = finalColor;

        //Sets as new color
        //GetComponent<Renderer>().material.color = collisionColor;

        // Waits for set duration
        //yield return new WaitForSeconds(colorDuration);

        //Sets as initial color
        //GetComponent<Renderer>().material.color = initialColor;
    }
}
