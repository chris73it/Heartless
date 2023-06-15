using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameRoate : MonoBehaviour
{

     float rotationSpeed = 500f;
         float spinSpeed = 1250f;
    

   
    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Spin the object around its Y-axis
        transform.rotation *= Quaternion.Euler(0f, spinSpeed * Time.deltaTime, 0f);
    }
}
