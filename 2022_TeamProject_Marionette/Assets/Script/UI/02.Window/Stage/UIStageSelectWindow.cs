using UnityEngine;
using UnityEngine.UI;
using BaseFrame;

namespace Marionette
{
    public class UIStageSelectWindow : UIBase
    {
        #region SerializeField
        [SerializeField]
        Text m_textTitle = null;

        #endregion

        uint m_icurChapter = 1;

        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }


        #region Btn_Event
        public void OnClickPrevChapter()
        {
            Debug.Log("PrevChapter");
        }

        public void OnClickNextChapter()
        {
            Debug.Log("NextChapter");
        }

        public void OnClickBackWindow()
        {
            UIManager.Instance.CloseUI<UIStageSelectWindow>();
            UIManager.Instance.OpenUI<UIMainWindow>();
        }
        #endregion
    }
}
