using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Buttons : MonoBehaviour
{
    public string sceneName;
    public int lvl;


    void Start()
    {
    
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.LogWarning("Application has quit.");
    }
}