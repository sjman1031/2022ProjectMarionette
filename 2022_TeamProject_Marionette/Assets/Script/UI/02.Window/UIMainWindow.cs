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

        public void OnClickOpenHowToPlay()
        {
            Debug.Log("Open UIHowToPlayPopup");
        }

        public void OnClickOpenShop()
        {
            Debug.Log("Open UIShopWindow");
        }

        public void OnClickOpenOption()
        {
            Debug.Log("Open UIOptionPopup");
        }

        public void OnClickOpenExit()
        {
            Debug.Log("Game Exit");
        }
        #endregion
    }
}