using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaseFrame;

namespace Marionette
{
    public class GameManager : MonoSingletonObject<GameManager>
    {
        public int iSceneLoadCnt = 0;

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();

            s_instance = this;

            initManager();
            ReadData();

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().name == "GameStartScene" && iSceneLoadCnt == 0)
            {
                UIManager.Instance.OpenUI<UIStartWindow>();
                iSceneLoadCnt++;
            }
            else if (SceneManager.GetActiveScene().name == "GameStartScene" && iSceneLoadCnt > 0)
                UIManager.Instance.OpenUI<UIMainWindow>();
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
            //MoveManager.Init();
            UIManager.Init();
            DataManager.Init();
        }

        public void ReadData()
        {
            DataManager.Instance.StartLoad();
        }

        //2022.11.04 한택 : enum eScene순서는 빌드할때 Scene 순서와 맞춰서 넣을것
        public void MoveScene(eScene sceneName, bool bRememberUIBeforeScene = false)
        {
            UIManager.Instance.MoveScene(bRememberUIBeforeScene);
            SceneManager.LoadScene((int)sceneName);
        }       
    }

}