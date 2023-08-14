using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PanelScript : MonoBehaviour
{
    private GameObject text;
    private TextMeshProUGUI textMesh;
    private Color tempColor;

    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void setTextColor(bool alive)
    {
        if (alive)
        {
            text = gameObject.transform.GetChild(1).gameObject;
            textMesh = text.GetComponent<TextMeshProUGUI>();
            if(textMesh != null)
            {
                tempColor = textMesh.color;
                tempColor.a = 255;
                textMesh.color = tempColor;
            }
        }
        else
        {
            text = gameObject.transform.GetChild(0).gameObject;
            textMesh = text.GetComponent<TextMeshProUGUI>();
            if (textMesh != null)
            {
                tempColor = textMesh.color;
                tempColor.a = 255;
                textMesh.color = tempColor;
            }
        }
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
