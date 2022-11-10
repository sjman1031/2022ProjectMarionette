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
                Debug.Log("���ӿ���");
                GameOver();
            }
            if (collision.gameObject.tag == "GreenObject")
            {
                Time.timeScale = 0;
                Debug.Log("Ŭ����");
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