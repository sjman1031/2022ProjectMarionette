using Marionette.Core;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Marionette.Data
{
    public struct Game_Data
    {
        public float handPower;
        public float footPower;
        public float moveSpeed;
        public float waitTime;
    }


    public class DataManager : MonoBehaviour
    {
        private static DataManager instance = null;

        private void Awake()
        {
            if (null == instance)
            {
                instance = this;

                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);
        }


        public static DataManager Instance
        {
           get 
            { 
                if (null == instance) 
                    return null; 
                return instance; 
            }
        }

        public Game_Data data;

        private void Start()
        {
            string jsonData = Resources.Load("Data/Data").ToString();
            data = JsonUtility.FromJson<Game_Data>(jsonData);
        }

        public void Load_Data()
        {
            Start();

            GameManager.Instance.handPower = data.handPower;
            GameManager.Instance.footPower = data.footPower;
            GameManager.Instance.moveSpeed = data.moveSpeed;
        }

    }
}

