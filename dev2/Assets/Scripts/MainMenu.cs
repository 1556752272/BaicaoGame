using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstLevelName;
    
    public void StartGame()
    {
        Debug.Log("start");
        SceneManager.LoadScene(1);
        // SceneManager.LoadScene(firstLevelName);
    }
    public void StartGame2()
    {
        Debug.Log("start");
        SceneManager.LoadScene(2);
        // SceneManager.LoadScene(firstLevelName);
    }
    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
