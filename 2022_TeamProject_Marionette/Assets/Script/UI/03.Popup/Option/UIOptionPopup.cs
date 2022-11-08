using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public class UIOptionPopup : UIBase
    {

        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }


        #region Btn_Event
        public void OnClickUIOptionPopup()
        {
            Debug.Log("Open UIOptionPopup");
            UIManager.Instance.CloseUI<UIMainWindow>();
            UIManager.Instance.OpenUI<UIOptionPopup>();
        }

        public void OnClickUIBack()
        {
            Debug.Log("close UIOptionPopup");
            UIManager.Instance.CloseUI<UIOptionPopup>();
            UIManager.Instance.OpenUI<UIMainWindow>();
        }
        #endregion
    }
}
