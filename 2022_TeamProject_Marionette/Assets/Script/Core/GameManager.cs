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
            ReadData();

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