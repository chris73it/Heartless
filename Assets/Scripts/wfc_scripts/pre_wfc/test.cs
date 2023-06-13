using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        //Destroy(gameObject,3f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            //Destroy(gameObject);

            //rb.AddForce(Vector3.up * 500);
        }

        //rb.velocity = Vector3.forward * 20f;
    }

    private void OnMouseDown() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            //Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
