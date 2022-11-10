using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public interface IDataParam 
    {

    }

    public class TableData : IDataParam
    {
        public struct Player_Data
        {
            public float moveSpeed;
            public float jumpSpeed;
        }

        Player_Data player_Data;

        public List<float> GetPlayerData()
        {
            return player_Data;
        }
    }
}