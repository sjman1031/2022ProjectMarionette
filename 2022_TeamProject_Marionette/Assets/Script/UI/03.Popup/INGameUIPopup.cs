using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaseFrame;

namespace Marionette
{
    public class INGameUIPopup : UIBase
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
            SceneManager.LoadScene("GameStartScene");
            UIManager.Instance.OpenUI<UIMainWindow>();
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
    }
}