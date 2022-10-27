using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarScoreProgressBarSkin : UI_Template
{
    [SerializeField]
    protected Image[] stars;
    [SerializeField]
    protected Image barBk;
    [SerializeField]
    protected Image barFill;

    protected override void LoadUISkin()
    {

        base.LoadUISkin();

        UpdateImage(barBk,currentUISkin.starScoreProgressBarBK);
        UpdateImage(barFill, currentUISkin.starScoreProgressBarFill);

    }

    public void UpdateStar(int id, bool isOn)
    {
        if (isOn)
            stars[id].sprite = currentUISkin.GetIcon(UI_Skin.Icon.StarOn);
        else
            stars[id].sprite = currentUISkin.GetIcon(UI_Skin.Icon.StarOff);
    }
}
