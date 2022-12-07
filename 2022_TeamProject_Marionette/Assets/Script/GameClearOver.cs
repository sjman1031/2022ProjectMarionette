using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marionette
{
    public class GameClearOver : MonoBehaviour
    {
        public UIClearPopup Cl;
        public UIOverPopup Ov;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "RedObject")
            {
                Debug.Log("게임오버");
                
                Ov.OverPopup();
            }
            else if (other.tag == "GreenObject")
            {
                Debug.Log("게임클리어");
                
                Cl.ClearPopup();

            }
            
        }

    }

}
