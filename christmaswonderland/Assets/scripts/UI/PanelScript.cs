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
    GameObject joystick;
    GameObject playerObj;
    GameObject resumeButton;
    playermovement player;


    void Start() {
        resumeButton = gameObject.transform.GetChild(2).gameObject;
        joystick = transform.parent.transform.GetChild(1).gameObject;
    }

    void Update() {
        if (playerObj == null) {
            playerObj = GameObject.FindWithTag("Player");
            player = playerObj.GetComponent<playermovement>();
        }
    }

    public void enable()
    {
        gameObject.SetActive(true);
    }

    public void disable()
    {
        gameObject.SetActive(false);
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

    public void resumeGame()
    {
        if (joystick != null) joystick.SetActive(true);
        if (resumeButton != null) resumeButton.SetActive(false);
        Time.timeScale = 1f;
        if (player != null) player.setIsPause(false);
        disable();
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
