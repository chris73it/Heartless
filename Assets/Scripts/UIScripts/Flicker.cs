using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour
{
    public Image lightImage;
    public float delayTime = 0.03f;
   
    void Start()
    {
        lightImage.color = new Vector4(255, 255, 255, 0.1f);
        FlickStart();
    }

    public IEnumerator Flick(float duration)
    {
      

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lightImage.color = new Vector4(255, 255, 255, 0.1f);

        StartCoroutine(Dim(delayTime));
    }
    public IEnumerator Dim(float duration)
    {
      

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        lightImage.color = new Vector4(255, 255, 255, 0.08f);

        StartCoroutine(Flick(delayTime));
    }
    public void FlickStart()
    {
        StartCoroutine(Flick(delayTime));
    }
   

    

}
