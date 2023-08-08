using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batflap : MonoBehaviour
{
    public AudioSource batflap;
    public void flapwings()
    {
        batflap.PlayOneShot(batflap.clip);
    }
}

