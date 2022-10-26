using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileButtonSkin : UI_Template
{

    [SerializeField]
    protected profile_button my_profile_button;

    [SerializeField]
    protected Image myImageOn;
    [SerializeField]
    protected Image myImageOff;
    [SerializeField]
    protected TextMeshProUGUI myNameOnText;
    [SerializeField]
    protected TextMeshProUGUI myNameOffText;
    [SerializeField]
    protected TextMeshProUGUI myNumberText;
    [SerializeField]
    protected TextMeshProUGUI myLivesCountText;
    [SerializeField]
    protected TextMeshProUGUI myStarsCountText;
    [SerializeField]
    protected TextMeshProUGUI myProgressText;

    protected override void LoadUISkin()
    {
        base.LoadUISkin();


        myImageOn.sprite = currentUISkin.profileItemOn;
        myImageOff.sprite = currentUISkin.profileItemOff;

        my_profile_button.profile_on_sprite = currentUISkin.profileItemOn;
        my_profile_button.profile_selected_sprite = currentUISkin.profileItemSelected;


        UpdateText(myNameOnText, currentUISkin.profileItemName);
        UpdateText(myNameOffText, currentUISkin.profileItemName);
        UpdateText(myNumberText, currentUISkin.profileItemNumber);
        UpdateText(myLivesCountText, currentUISkin.profileItemCount);
        UpdateText(myStarsCountText, currentUISkin.profileItemCount);
        UpdateText(myProgressText, currentUISkin.profileItemProgress);


    }
}
