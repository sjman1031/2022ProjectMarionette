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
                Debug.Log("���ӿ���");
                
                Ov.OverPopup();
            }
            else if (other.tag == "GreenObject")
            {
                Debug.Log("����Ŭ����");
                
                Cl.ClearPopup();

            }
            
        }

    }

}
