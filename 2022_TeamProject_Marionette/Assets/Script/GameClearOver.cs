using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marionette
{
    public class GameClearOver : MonoBehaviour
    {
        public INGameUIPopup IG;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "RedObject")
            {
                Time.timeScale = 0;
                Debug.Log("게임오버");
                GameOver();
            }
            if (collision.gameObject.tag == "GreenObject")
            {
                Time.timeScale = 0;
                Debug.Log("클리어");
                GameClear();
            }
        }

        void GameOver()
        {
            IG.GameOver();
        }
        void GameClear()
        {
            IG.GameClear();
        }
    }
}