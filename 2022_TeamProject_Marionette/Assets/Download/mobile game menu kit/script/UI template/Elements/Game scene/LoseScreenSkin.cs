using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenSkin : UI_Template
{
    [SerializeField]
    protected Image myBk;
    [SerializeField]
    protected Image myEmoticon;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myBk)
            UpdateImage(myBk, currentUISkin.loseScreenBK);

        if (myEmoticon)
            myEmoticon.sprite = currentUISkin.GetEmoticon(UI_Skin.EndScreenEmotion.Sad);



    }
}
