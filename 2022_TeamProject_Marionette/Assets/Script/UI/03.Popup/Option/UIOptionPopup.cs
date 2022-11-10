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
        public void OnClickCloseUIOptionPopup()
        {
            Debug.Log("close UIOptionPopup");
            UIManager.Instance.CloseUI<UIOptionPopup>();
            UIManager.Instance.OpenUI<UIMainWindow>();
        }
        #endregion
    }
}
