using UnityEngine;
using UnityEngine.UI;
using BaseFrame;

namespace Marionette
{
    public class UIOverPopup : UIBase
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

        public GameObject Over;

        public void OverPopup()
        {
            UIManager.Instance.OpenUI<UIOverPopup>();
        }
    }
}
