using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroicArcade.CC.Core
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] float verticalSmooth;
        [SerializeField] float minVerticalCameraPos;
        [SerializeField] float maxVerticalCameraPos;
        //[SerializeField] float minVerticalCameraRot;
        //[SerializeField] float maxVerticalCameraRot;

        GameObject avatar;
        Player player;
        float jumpVelocity;
        float cameraJumpVelocity;
        float cameraRotVelocity;
        Vector3 cameraPosition;
        //Quaternion cameraRotation;
        void Awake()
        {
            avatar = GameObject.Find("Dronion");

        }

        void Start()
        {
            player = avatar.GetComponent<Player>();
            jumpVelocity = player.jumpVelocity;
            cameraJumpVelocity = jumpVelocity * verticalSmooth;
            Debug.Log(jumpVelocity);

        }


        void Update()
        {
            cameraPosition = transform.position;
            if (avatar.transform.position.y > transform.position.y)
            {
                cameraPosition.y += cameraJumpVelocity * Time.deltaTime;
                //cameraRotation.y += cameraJumpVelocity * Time.deltaTime;

            }
            else if (avatar.transform.position.y < transform.position.y)
            {
                cameraPosition.y -= cameraJumpVelocity * Time.deltaTime;
            }

            //if(Mathf.Approximately( cameraPosition.y, 2.9f))
            if (cameraPosition.y < minVerticalCameraPos)
            {
                cameraPosition.y = minVerticalCameraPos;
            }
            else if (cameraPosition.y > maxVerticalCameraPos)
            {
                cameraPosition.y = maxVerticalCameraPos;
            }

            transform.position = cameraPosition;
        }
    }
}