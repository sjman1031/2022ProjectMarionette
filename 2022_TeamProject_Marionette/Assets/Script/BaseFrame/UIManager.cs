using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{

    public class UIManager : SingletonT<UIManager>
    {
        const eResourceType m_eType = eResourceType.UI;

        //20.04.15 HT^^ : 한번이라도 열린 UI들은 여기로
        Dictionary<eUIObject, UIBase> m_dicUI = null;

        //stack 현재 열린 UI들은 여기다가 집어넣고 닫힐때마다 빼자
        //stack 과 queue의 차이 
        //stack 후입선출 / queue 선입선출 기억좀하자
        Stack<UIBase> m_stackActiveUI = null;

        List<UIBase> m_lstPreSceneUI = null; 

        Canvas m_uiCanvas = null;

        RectTransform m_pageRoot = null;
        RectTransform m_windowRoot = null;
        RectTransform m_frontPageRoot = null;
        RectTransform m_popupRoot = null;

        RectTransform m_root = null;

        //UiRoot는 생성하지말고 각 씬마다 미리 만들어두고 찾도록 하자

        protected override void onInit()
        {
            base.onInit();

            m_dicUI = new Dictionary<eUIObject, UIBase>();
            m_stackActiveUI = new Stack<UIBase>();

            m_uiCanvas = GameObject.FindObjectOfType<Canvas>();

            var roots = m_uiCanvas.GetComponentsInChildren<RectTransform>();

            //임시 코드 리펙토링 후 주석 삭제
            m_pageRoot = roots[1];
            m_windowRoot = roots[2];
            m_frontPageRoot = roots[3];
            m_popupRoot = roots[4];
        }

        public void Clear()
        {
            onClear();
        }

        //팝업과 기본UI 구분하기
        public T OpenUI<T>(IUIOpenParam args = null) where T : UIBase
        {
            return doOpenUI(getEnumUIObject(typeof(T).Name), args) as T;
        }

        public T CloseUI<T>()where T : UIBase
        {
            return doCloseUI(getEnumUIObject(typeof(T).Name)) as T;
        }

        public UIBase OpenUI(eUIObject uiName) 
        {
            return doOpenUI(uiName);
        }

        public void CloseUI(eUIObject eUIName)
        {
            doCloseUI(eUIName);
        }

        public void MoveScene(bool bRememberUI = false)
        {
            List<UIBase> uiData = new List<UIBase>();

          
            for (int i = 0; i < m_stackActiveUI.Count; i++)
            {
                uiData.Add(m_stackActiveUI.Pop());
            }
           

            // UILoadingPage FadeOut



            // UILoadingPage FadeOut




            doAllCloseUI();
            onClear();
            
            if (bRememberUI)
                m_lstPreSceneUI = uiData;
        }

        //eUIObject 판별
        eUIObject getEnumUIObject(string objName)
        {
            //foreach 사용 
            foreach (eUIObject uiObject in Enum.GetValues(typeof(eUIObject)))
            {
                if (objName.CompareTo(uiObject.ToString()) == 0)
                    return uiObject;
            }

            return eUIObject.UIErrorPopup;
        }

        //UI 종류 별로 root 따로 만들것 !!
        eUIDefine setAdjustCanvas(eUIObject eUIName)
        {
            eUIDefine eType = (eUIDefine)((int)eUIName / 1000);

            if (eType == eUIDefine.Page)
            {
                m_root = m_pageRoot;
                return eUIDefine.Page;
            }
            else if (eType == eUIDefine.Window)
            {
                m_root = m_windowRoot;
                return eUIDefine.Window;
            }
            else if (eType == eUIDefine.FrontPage)
            {
                m_root = m_frontPageRoot;
                return eUIDefine.FrontPage;
            }
            else if(eType == eUIDefine.Popup)
            {
                m_root = m_popupRoot;
                return eUIDefine.Popup;
            }
            else
            {
                m_root = m_popupRoot;
                return eUIDefine.Notice;
            }
        }

        //20.04.15 HT^^[작업 완료] : UI 열기 
        UIBase doOpenUI(eUIObject uiName, IUIOpenParam args = null)
        {
            eUIDefine uiDefine = setAdjustCanvas(uiName);

            UIBase uiObject = null;
            if (m_dicUI.TryGetValue(uiName, out uiObject) != true)
            {
                //var strPath = string.Format(PrefabPath.UI, uiName); 안쓰는 코드
                var strPath = string.Format("{0}/{1}",uiDefine,uiName);
                uiObject = ResourceManager.Load<UIBase>(m_eType, strPath);

                uiObject = UnityEngine.Object.Instantiate<UIBase>(uiObject);
                uiObject.SetData(args);

                m_dicUI.Add(uiName, uiObject);

                //레이어를 나뉘어서 UI를 작업하려고 했는데...
                uiObject.transform.SetParent(m_root, false);
            }

            m_stackActiveUI.Push(uiObject);

            uiObject.SetActive(true);
            uiObject.transform.SetAsLastSibling();

            return uiObject;
        }
        
        //20.04.15 HT^^[작업 완료] : UI 닫기 
        UIBase doCloseUI(eUIObject uiName)
        {
            if (m_dicUI.TryGetValue(uiName, out var uiObject) != false)
            {
                if (uiObject.IsActive)
                {
                    if (m_stackActiveUI.Pop() == uiObject)
                    {
                        uiObject.SetActive(false);
                        return uiObject;
                    }
                }
            }
            else
            {
                //error!
            }

            return null;
        }

        void doAllCloseUI()
        {
            int iStackCount = m_stackActiveUI.Count;
            for (int i = 0; i < iStackCount; i++)
            {
                var uiObject = m_stackActiveUI.Pop();
                uiObject.Destroy();
            }
        }

        void onClear()
        {
            if (m_dicUI != null)
                m_dicUI.Clear();

            if (m_stackActiveUI != null)
                m_stackActiveUI.Clear();
        }


        #region TestCode
        public void SetFadeInUI()
        {

        }

        public void SetFadeOutUI()
        {

        }

        #endregion TestCode
    }
}
