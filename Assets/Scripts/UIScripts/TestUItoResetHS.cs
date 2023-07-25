using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUItoResetHS : MonoBehaviour
{
    private const string HIGH_SCORE_KEY = "HighScore";
    public bool resetHS;
    // Start is called before the first frame update
    void Start()
    {
        resetHS = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (resetHS == true)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);
            PlayerPrefs.Save();
            //resetHS = false;
        }
    }
}
