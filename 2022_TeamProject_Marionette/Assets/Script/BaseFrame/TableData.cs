using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFrame
{
    [System.Serializable]
    public class PlayerData : TableData
    {
        public float moveSpeed;
        public float jumpSpeed;
    }

    public class StageData : TableData
    {
    }

    [System.Serializable]
    public class TableData
    {
        public string tableName;


        public virtual void Load(string table)
        {
            //playerData = ResourceManager.Load<>
        }
    }
}