using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSkin : UI_Template
{

    public enum TextSize
    {
        small, 
        medium,
        big,
        huge 
    }
    public TextSize myFontSizes;
    TextMeshProUGUI myText;



    protected override void LoadUISkin()
    {
        if (myText == null)
            myText = GetComponent<TextMeshProUGUI>();

        base.LoadUISkin();

        UpdateText(myText, currentUISkin.defaultTexts[(int)myFontSizes]);

        /*
        switch (myTextSize)
        {
            case TextSize.small:
                UpdateText(myText, currentUISkin.smallText);
                break;

            case TextSize.medium:
                UpdateText(myText, currentUISkin.mediumText);
                break;

            case TextSize.big:
                UpdateText(myText, currentUISkin.bigText);
                break;

            case TextSize.huge:
                UpdateText(myText, currentUISkin.hugeText);
                break;

        }*/
    }
}
