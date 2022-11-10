using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public class CHARACTER_DATA : TableData 
    {
        public float moveSpeed;
        public float jumpSpeed;
    }

    public class Character : MonoSingletonObject<Character>
    { 

    }
}