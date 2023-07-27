using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toMainMenu : MonoBehaviour
{

    public void MainMenuReturn()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);


    }
}
