using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HeroicArcade.CC.Core
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 originalPosition;
        public float shakeTime = 0.4f;
        public float shakeMag = 0.05f;
        public bool isShaking = false;
        public bool isClosing;
        void Start()
        {

            //StartCoroutine(Shake(shakeTime, shakeMag));
        }

        public IEnumerator Shake(float duration, float magnitude)
        {
            originalPosition = transform.localPosition;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = originalPosition + new Vector3(x, y, 0f);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            transform.localPosition = originalPosition;
        }

        public void ShakeStart()
        {
            StartCoroutine(Shake(shakeTime, shakeMag));
        }
        public void DeathShakeStart()
        {
            StartCoroutine(Shake(shakeTime+ 0.3f, shakeMag *6));
            isShaking = true;
        }
        public void ClosingInOnYou( float time)
        {
            isClosing = true;
            StartCoroutine(Shake(time, shakeMag -0.01f));
        }
        public void ShakeSchtop()
        {
            StopCoroutine(Shake(shakeTime, shakeMag));
            //transform.localPosition = originalPosition;
        }
    }
   
}
