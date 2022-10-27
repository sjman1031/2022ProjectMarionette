using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFrame
{
    public interface IData
    {
        //public IData();
    }

    public class DataManager
    {
        public void Init_Data(string dataPath, IData data)
        {
            string jsonData = Resources.Load(dataPath).ToString();
            data = JsonUtility.FromJson<IData>(jsonData);
        }
    }
}