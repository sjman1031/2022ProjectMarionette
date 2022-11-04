using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaseFrame;

namespace Marionette
{
    public class GameManager : MonoSingletonObject<GameManager>
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            s_instance = this;

            initManager();

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            UIManager.Instance.OpenUI<UIStartWindow>();
        }

        protected override void OnDestroy()
        {

            base.OnDestroy();
        }

        protected override void OnApplicationQuit()
        {

            base.OnApplicationQuit();
        }
        #endregion MonoBehaviour


        void initManager()
        {
            ResourceManager.Init();
            UIManager.Init();
        }

        //2022.11.04 ���� : enum eScene������ �����Ҷ� Scene ������ ���缭 ������
        public void MoveScene(eScene sceneName, bool bRememberUIBeforeScene = false)
        {
            UIManager.Instance.MoveScene(bRememberUIBeforeScene);
            SceneManager.LoadScene((int)sceneName);
        }
    }

}