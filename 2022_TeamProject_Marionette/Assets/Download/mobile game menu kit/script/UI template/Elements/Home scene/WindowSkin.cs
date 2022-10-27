using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowSkin : UI_Template
{

    [SerializeField]
    protected Image myBK;

    [Space()]
    [SerializeField]
    protected TextMeshProUGUI myHeaderText;

    [SerializeField]
    protected TextMeshProUGUI[] myText;

    [SerializeField]
    protected TextMeshProUGUI[] myBigText;


    [Space()]
    [SerializeField]
    protected Image myScrollbarBK;
    [SerializeField]
    protected Image myScrollbarHandle;


    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        UpdateImage(myBK, currentUISkin.windowBK);

        if (myHeaderText)
            UpdateText(myHeaderText, currentUISkin.windowHeaderText);



        for (int i = 0; i < myText.Length; i++)
            {
            if (myText[i])
                UpdateText(myText[i], currentUISkin.windowText);
            }

        for (int i = 0; i < myBigText.Length; i++)
        {
            if (myText[i])
                UpdateText(myBigText[i], currentUISkin.windowBigText);

        }

        if (myScrollbarBK)
            UpdateImage(myScrollbarBK, currentUISkin.windowScrollbarBK);


        if (myScrollbarHandle)
            UpdateImage(myScrollbarHandle, currentUISkin.windowScrollbarHandle);

    }
}
