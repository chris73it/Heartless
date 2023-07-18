using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //audio
    public AudioSource HoverUI;
    public AudioSource SelectUI;

    public void PlayGame ()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
       
       
    }
    public void Hover()
    {

        HoverUI.PlayOneShot(HoverUI.clip);
    }
}
