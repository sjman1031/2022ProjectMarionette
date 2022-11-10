using UnityEngine;
using UnityEngine.SceneManagement;
using BaseFrame;

namespace Marionette
{
    public class UIStageCell : MonoBehaviour
    {



        #region Btn_Event
        public void OnClickStartStage()
        {
            SceneManager.LoadScene("Tropical Cliffs");
            Debug.Log("Start Stage");
            UIManager.Instance.CloseUI<UIStageSelectWindow>();
        }

        #endregion
    }
}