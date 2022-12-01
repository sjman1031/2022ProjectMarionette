using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marionette
{
    public class HeadOverJudgment : MonoBehaviour
    {
        public UIClearOverPopup CO;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Plan")
            {
                Debug.Log("게임오버");
                CO.OverPopup();
            }
        }
    }
}
