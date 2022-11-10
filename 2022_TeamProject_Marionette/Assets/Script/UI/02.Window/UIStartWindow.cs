using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public class UIStartWindow : UIBase
    {

        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }


        #region Btn_Event
        public void OnClickGameStart()
        {
            Debug.Log("Open UIMainWindow");
            UIManager.Instance.CloseUI<UIStartWindow>();
            UIManager.Instance.OpenUI<UIMainWindow>();
            
        }
        #endregion
    }
}
