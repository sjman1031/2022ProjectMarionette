using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionSoundElementSkin : UI_Template
{

    [SerializeField]
    protected Image myBk;
    [SerializeField]
    protected Image myIcon;
    [SerializeField]
    protected TextMeshProUGUI myText;
    [SerializeField]
    protected Image mySliderBk;
    [SerializeField]
    protected Image mySliderFill;
    [SerializeField]
    protected Image mySliderHandle;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myBk)
            UpdateImage(myBk, currentUISkin.optionSoundElementBK);

        if (mySliderBk)
            UpdateImage(mySliderBk, currentUISkin.optionSoundElementSliderBK);

        if (mySliderFill)
            UpdateImage(mySliderFill, currentUISkin.optionSoundElementSliderFill);

        if (mySliderHandle)
            UpdateImage(mySliderHandle, currentUISkin.optionSoundElementSliderHandle);

        if (myText)
            UpdateText(myText, currentUISkin.optionSoundElementText);

    }

    public void UpdateIcon(bool icoOn)
    {
        if (icoOn)
            myIcon.sprite = currentUISkin.GetIcon(UI_Skin.Icon.SoundOn);
        else
            myIcon.sprite = currentUISkin.GetIcon(UI_Skin.Icon.SoundOff);
    }
}
