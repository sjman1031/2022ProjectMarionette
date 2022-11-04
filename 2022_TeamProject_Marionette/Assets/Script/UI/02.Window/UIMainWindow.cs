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
        public void OnClickStageSelect()
        {
            Debug.Log("Open UIStageSelectWindow");
            UIManager.Instance.OpenUI<UIStageSelectWindow>();
        }

        public void OnClickHowToPlay()
        {
            Debug.Log("Open UIHowToPlayPopup");
        }

        public void OnClickShop()
        {
            Debug.Log("Open UIStageWindow");
        }

        public void OnClickOption()
        {
            Debug.Log("Open UIOptionPopup");
        }

        public void OnClickExit()
        {
            Debug.Log("Game Exit");
        }
        #endregion
    }
}