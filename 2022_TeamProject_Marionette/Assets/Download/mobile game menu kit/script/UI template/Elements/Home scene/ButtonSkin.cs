using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSkin : UI_Template
{
    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myText;
    [SerializeField]
    protected TextMeshProUGUI myCountText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        ButtonStatus(UI_Skin.ButtonStatus.On);

        if (myText)
            UpdateText(myText, currentUISkin.buttonText);

        if (myCountText)
            UpdateText(myCountText, currentUISkin.buttonCountText);

        
    }

    public void ButtonStatus(UI_Skin.ButtonStatus myStatus)
        {
        if (myImage)
            {
            if (myStatus == UI_Skin.ButtonStatus.On)
                UpdateImage(myImage, currentUISkin.buttonSprite);
            else if (myStatus == UI_Skin.ButtonStatus.Off)
                UpdateImage(myImage, currentUISkin.buttonSpriteOff);
            else if (myStatus == UI_Skin.ButtonStatus.Selected)
                UpdateImage(myImage, currentUISkin.buttonSpriteSelected);
        }

        }
}
