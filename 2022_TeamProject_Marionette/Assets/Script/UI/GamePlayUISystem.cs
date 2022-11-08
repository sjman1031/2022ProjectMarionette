using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUISystem : MonoBehaviour
{
    public GameObject PauseGUI;
    public GameObject GameOverGUI;
    public GameObject ClearGUI;

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tropical Cliffs");
    }
    public void Maingo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Demo");
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseGUI.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        PauseGUI.SetActive(false);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverGUI.SetActive(true);
    }
    public void GameClear()
    {
        Time.timeScale = 0;
        ClearGUI.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedObject")
        {
            Time.timeScale = 0;
            Debug.Log("충돌");
            GameOver();
        }
        if (collision.gameObject.tag == "GreenObject")
        {
            Time.timeScale = 0;
            Debug.Log("성공");
            GameClear();
        }
    }

}
