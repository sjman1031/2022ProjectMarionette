using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScreenSkin : UI_Template
{

    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myImage)
            UpdateImage(myImage, currentUISkin.screenBK);

        if (myText)
            UpdateText(myText, currentUISkin.screenHeaderText);

    }
}
