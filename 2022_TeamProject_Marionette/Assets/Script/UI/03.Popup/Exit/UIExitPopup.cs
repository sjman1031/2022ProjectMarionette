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
        public void OnClickUIExitPopup()
        {
            Debug.Log("Open UIExitPopup");
            UIManager.Instance.CloseUI<UIMainWindow>();
            UIManager.Instance.OpenUI<UIExitPopup>();
        }

        public void OnClickNoBtn()
        {
            Debug.Log("Close UIExitPopup");
            UIManager.Instance.CloseUI<UIExitPopup>();
            UIManager.Instance.OpenUI<UIMainWindow>();
        }
        #endregion
    }
}
