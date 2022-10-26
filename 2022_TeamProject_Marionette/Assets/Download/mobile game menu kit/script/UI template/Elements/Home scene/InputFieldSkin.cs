using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldSkin : UI_Template
{

    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myText;
    [SerializeField]
    protected TextMeshProUGUI myPlaceholderText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        UpdateImage(myImage, currentUISkin.inputFieldBK);

        UpdateText(myText, currentUISkin.inputFieldText);


        if (myPlaceholderText)
            UpdateText(myPlaceholderText, currentUISkin.inputFieldText);


    }
}
