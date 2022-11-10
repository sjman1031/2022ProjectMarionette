using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public class Character_Data
    {
        private float moveSpeed;
        private float jumpSpeed;

        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }   
        public float JumpSpeed { get { return jumpSpeed; } set { jumpSpeed = value; } }
    }

    public class Character : MonoSingletonObject<Character>
    {
        public Character_Data charaData;  
    }
}