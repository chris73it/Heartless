using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpNDown : MonoBehaviour
{
    private Vector3 originalPosition;
    public float delayTime = 0.4f;
   
    void Start()
    {

        UpStart();
    }

    public IEnumerator Up(float duration)
    {
        originalPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition + new Vector3 (0, 6, 0);
        StartCoroutine(Down(delayTime));
    }
    public IEnumerator Down(float duration)
    {
        originalPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition - new Vector3(0, 6, 0);
        StartCoroutine(Up(delayTime));
    }
    public void UpStart()
    {
        StartCoroutine(Up(delayTime));
    }
   

    

}
