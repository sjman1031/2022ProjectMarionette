using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBarSkin : UI_Template
{
    [SerializeField]
    protected Image myBk;
    [SerializeField]
    protected Image myTimerBk;

    [SerializeField]
    protected TextMeshProUGUI[] myText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myBk)
            UpdateImage(myBk, currentUISkin.infoBarBK);


        if (myTimerBk)
            UpdateImage(myTimerBk, currentUISkin.infoBarTimerBK);


        for (int i = 0; i < myText.Length; i++)
            UpdateText(myText[i], currentUISkin.infoBarText);

    }
}
