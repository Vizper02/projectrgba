using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterselec : MonoBehaviour
{

    private int charselec;

    public void confirmselec()
    {
        SceneManager.LoadScene("gscene");
    }

    public void bobbyselec()
    {
        charselec = 1;
        PlayerPrefs.SetInt("CharacterSelected", charselec);
    }

    public void santaselec()
    {
        charselec = 2;
        PlayerPrefs.SetInt("CharacterSelected", charselec);
    }

    public void elfselec()
    {
        charselec = 3;
        PlayerPrefs.SetInt("CharacterSelected", charselec);
    }

    public void frankselec()
    {
        charselec = 4;
        PlayerPrefs.SetInt("CharacterSelected", charselec);
    }

    public void rudolphselec()
    {
        charselec = 5;
        PlayerPrefs.SetInt("CharacterSelected", charselec);
    }

    public void trunksselec()
    {
        charselec = 6;
        PlayerPrefs.SetInt("CharacterSelected", charselec);
    }
}
