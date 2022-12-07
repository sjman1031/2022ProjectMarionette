using UnityEngine;
using UnityEngine.UI;
using BaseFrame;

namespace Marionette
{
    public class UIClearOverPopup : UIBase
    {
        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }

        #region SerializeField
        [SerializeField]
        Text m_textTitle = null;
        [SerializeField]
        GameObject m_objGameClear = null;
        [SerializeField]
        GameObject m_objGameOver = null;
        #endregion

        public GameObject Clear;

        public GameObject Over;

        public void ClearPopup()
        {
            Clear.SetActive(true);
            Over.SetActive(false);
            UIManager.Instance.OpenUI<UIClearOverPopup>();
            Clear.SetActive(true);
            Over.SetActive(false);
        }

        public void OverPopup()
        {
            Clear.SetActive(false);
            Over.SetActive(true);
            UIManager.Instance.OpenUI<UIClearOverPopup>();
            Clear.SetActive(false);
            Over.SetActive(true);
        }
    }
}
