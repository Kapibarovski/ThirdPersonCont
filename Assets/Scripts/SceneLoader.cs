using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OpenPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OpenSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }
    
    public void OpenMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
