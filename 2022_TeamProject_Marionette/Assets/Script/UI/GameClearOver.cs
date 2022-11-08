using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearOver : MonoBehaviour
{
    public GamePlayUISystem GP;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedObject")
        {
            Time.timeScale = 0;
            Debug.Log("Ãæµ¹");
            GameOver();
        }
    }

    void GameOver()
    {
        GP.GameOver();
    }
}
