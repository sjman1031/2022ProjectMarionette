using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldButtonSkin : UI_Template
{
    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myWorldNameText;
    [SerializeField]
    protected TextMeshProUGUI myWorldNumberText;
    [SerializeField]
    protected TextMeshProUGUI myStarNeedText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        UpdateText(myWorldNameText, currentUISkin.worldButtonName);

        int worldNumber = GetComponent<world_ico_uGUI>().my_number;


        myImage.sprite = currentUISkin.worldButtonIcons[worldNumber];

        myWorldNumberText.text = (worldNumber+1).ToString();
        UpdateText(myWorldNumberText, currentUISkin.worldButtonNumber);

        if (myStarNeedText)
            UpdateText(myStarNeedText, currentUISkin.worldButtonStarsNeed);

    }

    public void RefreshMe()
    {
        LoadUISkin();

    }

}
