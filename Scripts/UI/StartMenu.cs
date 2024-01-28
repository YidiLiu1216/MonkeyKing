using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame() {
        Invoke("StartScene",1.2f);
    }
    public void ExitGame() {
        Application.Quit();
    }
    void StartScene() {
        SceneManager.LoadScene(4);
    }
}
