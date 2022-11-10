using UnityEngine;
using BaseFrame;

namespace Marionette
{

    public class UIExitPopup : UIBase
    {
        public void GameExit()
        {
            Application.Quit();
            Debug.Log("게임종료");
        }
        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }


        #region Btn_Event
        public void OnClickCloseExitPopup()
        {
            Debug.Log("Close UIExitPopup");
            UIManager.Instance.CloseUI<UIExitPopup>();
            UIManager.Instance.OpenUI<UIMainWindow>();
        }
        #endregion
    }
}
