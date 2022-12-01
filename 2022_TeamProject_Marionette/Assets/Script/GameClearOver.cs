using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marionette
{
    public class GameClearOver : MonoBehaviour
    {
        public UIClearOverPopup CO;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "RedObject")
            {
                Debug.Log("게임오버");
                CO.OverPopup ();
            }
            else if (other.tag == "GreenObject")
            {
                CO.ClearPopup();
                Debug.Log("게임클리어");
                
            }
            
        }

    }

}
