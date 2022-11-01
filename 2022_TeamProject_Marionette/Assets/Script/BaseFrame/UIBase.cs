using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaseFramework
{
    public interface IUIOpenParam
    {
        
    }

    public class UIBase : MonoBehaviour
    {
        //백그라운드 마땅한 변수 이름 생각안남
        #region SerializeField
        [SerializeField]
        Image m_imgBg = null;
        
        #endregion

        #region Properties
        public bool IsActive { get { return m_bActive; } }
        //public bool IsAnimCompleted { get { return m_bAnimCompleted; } }
        #endregion

        List<Image> m_lstImage;
        RectTransform m_rTransform = null;
        bool m_bActive = false;

        void Awake()
        {
            m_rTransform = GetComponent<RectTransform>();

        }

        public void SetData(IUIOpenParam args)
        {
            setData(args);

           
        }

        public void SetActive(bool bActive)
        {
            if (m_bActive != bActive)
            {
                gameObject.SetActive(bActive);
                m_bActive = bActive;
            }
        }

        
        public void Destroy()
        {
            SetActive(false);
            Destroy(gameObject);
        }

        protected virtual void setData(IUIOpenParam args)
        {

            m_lstImage = new List<Image>();
            var arrImage = gameObject.GetComponentsInChildren<Image>();
            for (int i = 0; i < arrImage.Length; i++)
            {
                m_lstImage.Add(arrImage[i]);
            }
        }

    }
}
