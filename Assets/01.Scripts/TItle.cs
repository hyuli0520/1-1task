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
    public GameObject helpPanel;

    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
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
        helpPanel.SetActive(true);
    }

    public void ClickBack()
    {
        optionPanel.SetActive(!optionPanel.activeSelf);
    }
    public void ClickOptionBack()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
        optionPanel.SetActive(!optionPanel.activeSelf);
    }
}
