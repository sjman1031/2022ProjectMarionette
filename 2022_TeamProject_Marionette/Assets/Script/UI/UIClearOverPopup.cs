using UnityEngine;
using UnityEngine.UI;
using BaseFrame;

namespace Marionette
{
    public class UIClearOverPopup : UIBase
    {
        #region SerializeField
        [SerializeField]
        Text m_textTitle = null;
        [SerializeField]
        GameObject m_objGameClear = null;
        [SerializeField]
        GameObject m_objGameOver = null; 
        #endregion


        protected override void setData(IUIOpenParam args)
        {
            base.setData(args);
        }

    }
}
