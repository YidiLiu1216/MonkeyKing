using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMananement : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameMananement instance { get; private set; }
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] AudioSource HappyEnd;
    [SerializeField] AudioSource WinEnd;
    [SerializeField] AudioSource GameBegine;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject GoPanel;
    [SerializeField] PlayerController P1;
    [SerializeField] PlayerController P2;
    void Awake()
    {
        instance = this;
        GameBegine.Play();
        Invoke("inactivestart", 1);
        Invoke("inactiveGopanel", 1.6f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void GameOver() {
        //set winner
        if (P1.maxHeight - P2.maxHeight > 0.5)
        {
            SceneManager.LoadScene(2);
        }
        else if (P2.maxHeight - P1.maxHeight > 0.5)
        {
            SceneManager.LoadScene(3);
        }
        else {
            if (P1.getHealth() > P2.getHealth())
            {
                SceneManager.LoadScene(2);

            }
            else {
                SceneManager.LoadScene(3);
            }
        }
        //freeze everything
        //pop the menu
        //gameOverMenu.SetActive(true);
        //WinEnd.Play();
        //HappyEnd.Play();
    }
    public void ResartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void inactivestart() {
        StartPanel.SetActive(false);
        GoPanel.SetActive(true);
    }
    void inactiveGopanel() {
        GoPanel.SetActive(false);
    }
   
}
