using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMaster : MonoBehaviour {

    public UI_Skin currentUISkin;
    public static SkinMaster THIS;

    ButtonSkin[] buttonSkins;
    IconSkin[] iconSkins;
    InfoBarSkin[] infoBarSkins;
    InputFieldSkin[] inputFieldSkins;
    OptionSoundElementSkin[] optionSoundElementSkin;
    ProfileButtonSkin[] profileButtonSkin;
    ScoreRecordScreenElementSkin[] scoreRecordScreenElementSkin;
    ScreenSkin[] screenSkins;
    StageButtonSkin[] stageButtonSkin;
    StoreButtonSkin[] storeButtonSkin;
    WindowSkin[] windowSkins;
    WorldButtonSkin[] worldButtonSkin;

    public virtual void Awake()
    {
        THIS = this;

    }

    public void ReskinAll()
    {
        if (currentUISkin == null)
            return;

        print("New skin: " + currentUISkin.name);

        buttonSkins = FindObjectsOfType<ButtonSkin>();
        for (int i = 0; i < buttonSkins.Length; i++)
            buttonSkins[i].currentUISkin = currentUISkin;

        iconSkins = FindObjectsOfType<IconSkin>();
        for (int i = 0; i < iconSkins.Length; i++)
            iconSkins[i].Reskin(currentUISkin);


        infoBarSkins = FindObjectsOfType<InfoBarSkin>();
        for (int i = 0; i < infoBarSkins.Length; i++)
            infoBarSkins[i].Reskin(currentUISkin);

        inputFieldSkins = FindObjectsOfType<InputFieldSkin>();
        for (int i = 0; i < inputFieldSkins.Length; i++)
            inputFieldSkins[i].Reskin(currentUISkin);

        optionSoundElementSkin = FindObjectsOfType<OptionSoundElementSkin>();
        for (int i = 0; i < optionSoundElementSkin.Length; i++)
            optionSoundElementSkin[i].Reskin(currentUISkin);

        profileButtonSkin = FindObjectsOfType<ProfileButtonSkin>();
        for (int i = 0; i < profileButtonSkin.Length; i++)
            profileButtonSkin[i].Reskin(currentUISkin);

        scoreRecordScreenElementSkin = FindObjectsOfType<ScoreRecordScreenElementSkin>();
        for (int i = 0; i < scoreRecordScreenElementSkin.Length; i++)
            scoreRecordScreenElementSkin[i].Reskin(currentUISkin);

        screenSkins = FindObjectsOfType<ScreenSkin>();
        for (int i = 0; i < screenSkins.Length; i++)
            screenSkins[i].Reskin(currentUISkin);

        stageButtonSkin = FindObjectsOfType<StageButtonSkin>();
        for (int i = 0; i < stageButtonSkin.Length; i++)
            stageButtonSkin[i].Reskin(currentUISkin);

        storeButtonSkin = FindObjectsOfType<StoreButtonSkin>();
        for (int i = 0; i < storeButtonSkin.Length; i++)
            storeButtonSkin[i].Reskin(currentUISkin);

        windowSkins = FindObjectsOfType<WindowSkin>();
        for (int i = 0; i < windowSkins.Length; i++)
            windowSkins[i].Reskin(currentUISkin);

        worldButtonSkin = FindObjectsOfType<WorldButtonSkin>();
        for (int i = 0; i < worldButtonSkin.Length; i++)
            worldButtonSkin[i].Reskin(currentUISkin);

    }
}
