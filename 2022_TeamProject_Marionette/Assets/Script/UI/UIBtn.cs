using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Marionette
{
    public enum BTNType
    {
        New,
        Continue,
        control,
        Option,
        Shop,
        Exit,
        Back,
        Start,
        StageBack
    }

    public class UIBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public BTNType currentType;
        public Transform buttonScale;
        Vector3 defaultScale;
        public CanvasGroup mainGroup;
        public CanvasGroup subGroup;
        public CanvasGroup startGroup;
        public CanvasGroup stageGroup;
        private void Start()
        {
            defaultScale = buttonScale.localScale;
        }
        public void OnBtnclick()
        {
            switch (currentType)
            {
                case BTNType.New:
                    CanvasGroupOn(stageGroup);
                    CanvasGroupOff(mainGroup);

                    break;
                case BTNType.control:
                    CanvasGroupOn(subGroup);
                    CanvasGroupOff(mainGroup);

                    Debug.Log("조작법");
                    break;
                case BTNType.Option:
                    CanvasGroupOn(subGroup);
                    CanvasGroupOff(mainGroup);

                    Debug.Log("설정");
                    break;
                case BTNType.Shop:
                    CanvasGroupOn(subGroup);
                    CanvasGroupOff(mainGroup);

                    Debug.Log("상점");
                    break;
                case BTNType.Exit:


                    Debug.Log("종료");
                    break;
                case BTNType.Back:
                    CanvasGroupOn(mainGroup);
                    CanvasGroupOff(subGroup);

                    Debug.Log("뒤로가기");
                    break;

                case BTNType.Start:
                    CanvasGroupOn(mainGroup);
                    CanvasGroupOff(startGroup);

                    Debug.Log("게임시작!");
                    break;

                case BTNType.StageBack:
                    CanvasGroupOn(mainGroup);
                    CanvasGroupOff(stageGroup);

                    Debug.Log("뒤로가기");
                    break;
            }
        }

        public void CanvasGroupOn(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;

        }
        public void CanvasGroupOff(CanvasGroup cg)
        {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonScale.localScale = defaultScale * 1.2f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonScale.localScale = defaultScale;
        }



    }

}