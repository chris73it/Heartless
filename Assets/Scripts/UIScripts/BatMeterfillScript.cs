using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{

    public class BatMeterfillScript : MonoBehaviour
    {

        public GameObject batDronion;

        public GameObject manDronion;

        public GameObject player;

        public GameObject batmeterFill;

        Vector3 blah;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (batDronion.activeInHierarchy == true)
            {

                blah.x = batmeterFill.transform.localScale.x - batDronion.GetComponent<BatTimer>().elapsedTime * 0.079f * Time.deltaTime; //ther 0.0000djsfdiosfno thing is scaling the icon aty a close looking rate
                blah.y = 1;
                blah.z = 1;
                batmeterFill.transform.localScale = blah;
            
                if (batmeterFill.transform.localScale.x < 0)
                {
                    batmeterFill.transform.localScale = new Vector3 (0,1,1);
                }


            }
            else if (player.GetComponent<Player>().death == false )
            {
                blah.x = batmeterFill.transform.localScale.x + manDronion.GetComponent<BatTimer>().elapsedTime * 0.00495f * Time.deltaTime; //ther 0.0000djsfdiosfno thing is scaling the icon aty a close looking rate
                blah.y = 1;
                blah.z = 1;
                batmeterFill.transform.localScale = blah;

                if (batmeterFill.transform.localScale.x > 1)
                {
                    batmeterFill.transform.localScale = new Vector3(1, 1, 1);
                }
            }








        }
    }
}
