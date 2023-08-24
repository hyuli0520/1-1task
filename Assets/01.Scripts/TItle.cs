using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TItle : MonoBehaviour
{
    public string sceneName = "GameStage";

    public GameObject optionPanel;
    public GameObject helpPanel1;
    public GameObject helpPanel2;

    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void ClickOption()
    {
        optionPanel.SetActive(true);
    }

    public void ClickExt()
    {
        Application.Quit();
    }

    public void ClickHelp()
    {
        optionPanel.SetActive(false);
        helpPanel1.SetActive(true);
    }

    public void ClickBack()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }
    public void ClickOptionBack()
    {
        helpPanel1.SetActive(false);
        helpPanel2.SetActive(false);
        optionPanel.SetActive(!optionPanel.activeSelf);
    }
    
    public void ClickPage()
    {
        helpPanel1.SetActive(!helpPanel1.activeSelf);
        helpPanel2.SetActive(!helpPanel2.activeSelf);
    }
}
