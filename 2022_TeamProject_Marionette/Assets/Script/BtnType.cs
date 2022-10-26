using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        switch(currentType)
        {
            case BTNType.New:
                CanvasGroupOn(stageGroup);
                CanvasGroupOff(mainGroup);              

                break;
            case BTNType.control:
                CanvasGroupOn(subGroup);
                CanvasGroupOff(mainGroup);

                Debug.Log("���۹�");
                break;
            case BTNType.Option:
                CanvasGroupOn(subGroup);
                CanvasGroupOff(mainGroup);

                Debug.Log("����");
                break;
            case BTNType.Shop:
                CanvasGroupOn(subGroup);
                CanvasGroupOff(mainGroup);

                Debug.Log("����");              
                break;
            case BTNType.Exit:


                Debug.Log("����");
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(subGroup);

                Debug.Log("�ڷΰ���");
                break;

            case BTNType.Start:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(startGroup);

                Debug.Log("���ӽ���!");
                break;

            case BTNType.StageBack:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(stageGroup);

                Debug.Log("�ڷΰ���");
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
