using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pageDotSkin : UI_Template
{

    //public bool dotOn;

    [SerializeField]
    protected Image myImage;


    protected override void LoadUISkin()
    {

        base.LoadUISkin();

        //UpdateDot(false);

    }

    public void UpdateDot(bool dotOn)
    {
        if (currentUISkin != SkinMaster.THIS.currentUISkin)
            currentUISkin = SkinMaster.THIS.currentUISkin;

        if (dotOn)
            UpdateImage(myImage, currentUISkin.pageDotON);
        else
            UpdateImage(myImage, currentUISkin.pageDotOFF);

    }
}
