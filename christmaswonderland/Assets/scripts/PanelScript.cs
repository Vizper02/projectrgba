using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour
{
    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene("charselec");
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void exit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
