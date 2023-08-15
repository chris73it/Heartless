using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{

    public class BatTimer : MonoBehaviour
    {


        private Vector3 originalPosition;
       // public float batTime = 5f;
        public bool endBat = false;
        //public bool rerun = false;
        public float elapsedTime = 0f;


        void Start()
        {

        }

        public IEnumerator BatCounterTimer(float duration)
        {


            elapsedTime = 0f;
           

            while (elapsedTime < duration)
            {

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            endBat = true;
          
        }

        public void BatStart(float batTime2)
        {
            StartCoroutine(BatCounterTimer(batTime2));
            
        }

    }
}