using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSkin : UI_Template
{

    public UI_Skin.Icon myIcon;
    protected Image myImage;


    private void Start()
    {
        
    }

    protected override void LoadUISkin()
    {
        if (myImage == null)
            myImage = GetComponent<Image>();

        base.LoadUISkin();

        myImage.sprite = currentUISkin.GetIcon(myIcon);

    }
}
