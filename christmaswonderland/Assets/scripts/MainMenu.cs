using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void Play()
    {
        SceneManager.LoadScene("charselec");
    }

  public void Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
