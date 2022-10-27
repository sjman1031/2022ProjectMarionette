using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreRecordScreenElementSkin : UI_Template
{
    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected TextMeshProUGUI myRankText;
    [SerializeField]
    protected TextMeshProUGUI myNameText;
    [SerializeField]
    protected TextMeshProUGUI myRecordText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();

        if (myImage)
            UpdateImage(myImage, currentUISkin.scoreScreenElementBK);


        UpdateText(myRankText, currentUISkin.scoreRank);
        UpdateText(myNameText, currentUISkin.scoreName);
        UpdateText(myRecordText, currentUISkin.scoreRecord);


    }
}
