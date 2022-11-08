using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public class UIMainWindow : UIBase
    {
        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }

        #region Btn_Event
        public void OnClickOpenStageSelect()
        {
            Debug.Log("Open UIStageSelectWindow");
            UIManager.Instance.CloseUI<UIMainWindow>();
            UIManager.Instance.OpenUI<UIStageSelectWindow>();
        }

        public void OnClickOpenHowToPlayPopup()
        {
            Debug.Log("Open UIHowToPlayPopup");
            //UIManager.Instance.OpenUI<UIHowToPlayPopup>();
        }

        public void OnClickOpenShopPopup()
        {
            Debug.Log("Open UIShopWindow");
        }

        public void OnClickOpenOptionPopup()
        {
            Debug.Log("Open UIOptionPopup");
            UIManager.Instance.CloseUI<UIMainWindow>();
            UIManager.Instance.OpenUI<UIOptionPopup>();
        }

        public void OnClickOpenExitPopup()
        {
            Debug.Log("Game Exit");
            UIManager.Instance.CloseUI<UIMainWindow>();
            UIManager.Instance.OpenUI<UIExitPopup>();
        }
        #endregion
    }
}