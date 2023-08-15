using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmusicrandselect : MonoBehaviour
{
  public  GameObject track1;
    public GameObject track2;
    public GameObject track3;
   
    void Start()
    {
       int goobertrack = Random.Range(1, 4);
        //Debug.Log(goobertrack);

        if (goobertrack == 1)
        {
            track1.SetActive(true);
        }
        else if (goobertrack == 2)
        {
            track2.SetActive(true);
        }
        else if (goobertrack == 3)
        {
            track3.SetActive(true);
        }
        else
        {
            track1.SetActive(true);
        }
    }
}
