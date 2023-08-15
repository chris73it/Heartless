using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //audio
    public AudioSource HoverUI;
    public AudioSource SelectUI;
    private const string HIGH_SCORE_KEY = "HighScore";
    int highScore;
    public GameObject hsTxt;
    private TMP_Text HSDisplay;
  
    public void PlayGame ()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);  //"_Merged_MainGame_Scene");//SceneManager.GetActiveScene().buildIndex +1);
       
       
    }
    public void PlayGamefromtest()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);  //"_Merged_MainGame_Scene");//SceneManager.GetActiveScene().buildIndex +1);


    }
    public void HiddenUI()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);  //"_Merged_MainGame_Scene");//SceneManager.GetActiveScene().buildIndex +1);

    }
    public void returnUI()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);  //"_Merged_MainGame_Scene");//SceneManager.GetActiveScene().buildIndex +1);

    }
    public void Comic()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);  //"_Merged_MainGame_Scene");//SceneManager.GetActiveScene().buildIndex +1);

    }
    public void Comicreturntomenu()
    {
        SelectUI.PlayOneShot(SelectUI.clip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);  //"_Merged_MainGame_Scene");//SceneManager.GetActiveScene().buildIndex +1);

    }
    public void Hover()
    {
        HoverUI.PlayOneShot(HoverUI.clip);
    }
    public void Options()
    {
    }
    public void Quit()
    {
        Debug.Log("endagem");
        Application.Quit();
    }
    public void HighScore()
    {
        hsTxt = GameObject.Find("HS display Text");
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        HSDisplay = hsTxt.GetComponent<TMP_Text>();
        HSDisplay.text = highScore.ToString() + "m";
    }
    public void Story()
    {
    }
    public void HowTo()
    {
    }
    public void Back()
    {
    }
}
