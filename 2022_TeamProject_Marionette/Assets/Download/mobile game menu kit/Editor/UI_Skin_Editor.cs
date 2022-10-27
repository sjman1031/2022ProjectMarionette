using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using TMPro;

[CustomEditor(typeof(UI_Skin))]
public class UI_Skin_Editor : Editor
{

    public override void OnInspectorGUI()
    {

        UI_Skin my_target = (UI_Skin)target;
        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(my_target, "skin");

        
        my_target.showText = EditorGUILayout.Foldout(my_target.showText, "Text");
        if (my_target.showText)
        {
            my_target.defaultFont = EditorGUILayout.ObjectField("defaultFont", my_target.defaultFont, typeof(TMP_FontAsset), true) as TMP_FontAsset;
            my_target.defaultFontColor = EditorGUILayout.ColorField("color", my_target.defaultFontColor);

            EditorGUILayout.LabelField("sizes:");
            EditorGUI.indentLevel++;
            for (int i = 0; i < my_target.fontSizes.Length; i++)
                {
                my_target.fontSizes[i] = EditorGUILayout.IntField(Enum.GetName(typeof(UI_Skin.FontSizes), i), my_target.fontSizes[i]);
                my_target.defaultTexts[i].font = my_target.defaultFont;
                my_target.defaultTexts[i].size = my_target.fontSizes[i];
                my_target.defaultTexts[i].color = my_target.defaultFontColor;
            }
            EditorGUI.indentLevel--;

            

  
}

        my_target.showIcons = EditorGUILayout.Foldout(my_target.showIcons, "Icons");
        if (my_target.showIcons)
        {
            if (my_target.icons.Length < Enum.GetNames(typeof(UI_Skin.Icon)).Length)
            {
                Sprite[] tempSprites = new Sprite[my_target.icons.Length];
                for (int i = 0; i < tempSprites.Length; i++)
                    tempSprites[i] = my_target.icons[i];

                my_target.icons = new Sprite[Enum.GetNames(typeof(UI_Skin.Icon)).Length];
                for (int i = 0; i < tempSprites.Length; i++)
                    my_target.icons[i] = tempSprites[i];
            }
            else if (my_target.icons.Length > Enum.GetNames(typeof(UI_Skin.Icon)).Length)
            {
                Sprite[] tempSprites = new Sprite[my_target.icons.Length];
                for (int i = 0; i < tempSprites.Length; i++)
                    tempSprites[i] = my_target.icons[i];

                my_target.icons = new Sprite[Enum.GetNames(typeof(UI_Skin.Icon)).Length];
                for (int i = 0; i < Enum.GetNames(typeof(UI_Skin.Icon)).Length; i++)
                    my_target.icons[i] = tempSprites[i];
            }

            for (int i = 0; i < Enum.GetNames(typeof(UI_Skin.Icon)).Length; i++)
                my_target.icons[i] = EditorGUILayout.ObjectField(Enum.GetName(typeof(UI_Skin.Icon), i), my_target.icons[i], typeof(Sprite), true) as Sprite;
        }



        my_target.showWindow = EditorGUILayout.Foldout(my_target.showWindow, "Window");
        if (my_target.showWindow)
        {
            EditorGUILayout.LabelField("Sprites:");
            EditorGUI.indentLevel++;
            ShowImageTemplate("windowBK", my_target.windowBK);
            EditorGUILayout.LabelField("windowScrollbar:");
                EditorGUI.indentLevel++;
                ShowImageTemplate("BK",my_target.windowScrollbarBK);
                ShowImageTemplate("Handle",my_target.windowScrollbarHandle);
                EditorGUI.indentLevel--;
            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("Text:");
                EditorGUI.indentLevel++;
                ShowTextTemplate("windowHeaderText", my_target.windowHeaderText);
                ShowTextTemplate("windowText", my_target.windowText);
                ShowTextTemplate("windowBigText", my_target.windowBigText);
                EditorGUI.indentLevel--;
        }


        my_target.showInputField = EditorGUILayout.Foldout(my_target.showInputField, "Input Field");
        if (my_target.showInputField)
        {
            ShowImageTemplate("inputFieldBK", my_target.inputFieldBK);
            ShowTextTemplate("inputFieldText", my_target.inputFieldText);
        }


        my_target.showScreen = EditorGUILayout.Foldout(my_target.showScreen, "Screen");
        if (my_target.showScreen)
        {
            ShowImageTemplate("screenBK", my_target.screenBK);
            ShowTextTemplate("screenHeaderText", my_target.screenHeaderText);
        }


        my_target.showButton = EditorGUILayout.Foldout(my_target.showButton, "Button");
        if (my_target.showButton)
        {
            ShowImageTemplate("buttonSprite", my_target.buttonSprite);
            ShowImageTemplate("buttonSpriteSelected", my_target.buttonSpriteSelected);
            ShowImageTemplate("buttonSpriteOff", my_target.buttonSpriteOff);

            ShowTextTemplate("buttonText", my_target.buttonText);
            ShowTextTemplate("buttonCountText", my_target.buttonCountText);
        }



        my_target.showOptionSoundElement = EditorGUILayout.Foldout(my_target.showOptionSoundElement, "Option Sound Element");
        if (my_target.showOptionSoundElement)
        {
            ShowImageTemplate("optionSoundElementBK", my_target.optionSoundElementBK);
            ShowImageTemplate("optionSoundElementSliderBK", my_target.optionSoundElementSliderBK);
            ShowImageTemplate("optionSoundElementSliderFill", my_target.optionSoundElementSliderFill);
            ShowImageTemplate("optionSoundElementSliderHandle", my_target.optionSoundElementSliderHandle);

            ShowTextTemplate("optionSoundElementText", my_target.optionSoundElementText);
        }


        my_target.showInfoBar = EditorGUILayout.Foldout(my_target.showInfoBar, "InfoBar");
        if (my_target.showInfoBar)
        {
            ShowImageTemplate("infoBarBK", my_target.infoBarBK);
            ShowImageTemplate("infoBarTimerBK", my_target.infoBarTimerBK);

            ShowTextTemplate("infoBarText", my_target.infoBarText);
        }



        my_target.showStageButton = EditorGUILayout.Foldout(my_target.showStageButton, "Stage Button");
        if (my_target.showStageButton)
        {
            my_target.stageButtonOn = EditorGUILayout.ObjectField("stageButtonOn", my_target.stageButtonOn, typeof(Sprite), true) as Sprite;
            my_target.stageButtonOff = EditorGUILayout.ObjectField("stageButtonOff", my_target.stageButtonOff, typeof(Sprite), true) as Sprite;
            my_target.stageButtonNextStageToPlay = EditorGUILayout.ObjectField("stageButtonNextStageToPlay", my_target.stageButtonNextStageToPlay, typeof(Sprite), true) as Sprite;
            my_target.stageButtonTailDotOn = EditorGUILayout.ObjectField("stageButtonTailDotOn", my_target.stageButtonTailDotOn, typeof(Sprite), true) as Sprite;
            my_target.stageButtonTailDotOff = EditorGUILayout.ObjectField("stageButtonTailDotOff", my_target.stageButtonTailDotOff, typeof(Sprite), true) as Sprite;

            ShowTextTemplate("stageButtonNumber", my_target.stageButtonNumber);
            ShowTextTemplate("stageButtonStarCount", my_target.stageButtonStarCount);
        }

        my_target.showWorls = EditorGUILayout.Foldout(my_target.showWorls, "Worls");
        if (my_target.showWorls)
        {
            my_target.totalWorlds = EditorGUILayout.IntField("Total Worlds", my_target.totalWorlds);
            EditorGUILayout.LabelField("-Total Worlds- MUST be the same number that the one in Home > manage_audio > Game_manager > Worlds > Total Worlds");
            EditorGUILayout.Space();

            if (my_target.worldBackgrounds.Length != my_target.worldButtonIcons.Length)
            {
                my_target.worldBackgrounds = new Sprite[1];
                my_target.worldButtonIcons = new Sprite[1];
            }

            if (my_target.worldButtonIcons.Length < my_target.totalWorlds || my_target.worldBackgrounds.Length < my_target.totalWorlds)
            {
                Sprite[] temp = new Sprite[my_target.worldButtonIcons.Length];
                Sprite[] tempBK = new Sprite[my_target.worldBackgrounds.Length];
                for (int i = 0; i < my_target.worldButtonIcons.Length; i++)
                {
                    temp[i] = my_target.worldButtonIcons[i];
                    tempBK[i] = my_target.worldBackgrounds[i];
                }

                my_target.worldButtonIcons = new Sprite[my_target.totalWorlds];
                my_target.worldBackgrounds = new Sprite[my_target.totalWorlds];
                for (int i = 0; i < temp.Length; i++)
                {
                    my_target.worldButtonIcons[i] = temp[i];
                    my_target.worldBackgrounds[i] = temp[i];
                }
            }
            else if (my_target.worldButtonIcons.Length > my_target.totalWorlds || my_target.worldBackgrounds.Length > my_target.totalWorlds)
            {
                Sprite[] temp = new Sprite[my_target.worldButtonIcons.Length];
                Sprite[] tempBK = new Sprite[my_target.worldBackgrounds.Length];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = my_target.worldButtonIcons[i];
                    tempBK[i] = my_target.worldBackgrounds[i];
                }

                my_target.worldButtonIcons = new Sprite[my_target.totalWorlds];
                my_target.worldBackgrounds = new Sprite[my_target.totalWorlds];
                for (int i = 0; i < my_target.worldButtonIcons.Length; i++)
                {
                    my_target.worldButtonIcons[i] = temp[i];
                    my_target.worldBackgrounds[i] = tempBK[i];
                }
            }

            for (int i = 0; i < my_target.worldButtonIcons.Length; i++)
            {
                EditorGUILayout.LabelField("World " + i.ToString());
                    EditorGUI.indentLevel++;
                    my_target.worldButtonIcons[i] = EditorGUILayout.ObjectField("Icon ", my_target.worldButtonIcons[i], typeof(Sprite), true) as Sprite;
                    my_target.worldBackgrounds[i] = EditorGUILayout.ObjectField("BK ", my_target.worldBackgrounds[i], typeof(Sprite), true) as Sprite;
                    EditorGUI.indentLevel--;
            }


            ShowTextTemplate("worldButtonName", my_target.worldButtonName);
            ShowTextTemplate("worldButtonNumber", my_target.worldButtonNumber);
            ShowTextTemplate("worldButtonStarsNeed", my_target.worldButtonStarsNeed);
        }



        my_target.showPageDot = EditorGUILayout.Foldout(my_target.showPageDot, "PageDot");
        if (my_target.showPageDot)
        {
            ShowImageTemplate("pageDotON", my_target.pageDotON);
            ShowImageTemplate("pageDotOFF", my_target.pageDotOFF);
        }


        my_target.showScoreRecordScreenElement = EditorGUILayout.Foldout(my_target.showScoreRecordScreenElement, "showScoreRecordScreenElement");
        if (my_target.showScoreRecordScreenElement)
        {
            ShowImageTemplate("scoreScreenElementBK", my_target.scoreScreenElementBK);
            ShowTextTemplate("scoreRank", my_target.scoreRank);
            ShowTextTemplate("scoreName", my_target.scoreName);
            ShowTextTemplate("scoreRecord", my_target.scoreRecord);
        }



        my_target.showStoreItem = EditorGUILayout.Foldout(my_target.showStoreItem, "showStoreItem");
        if (my_target.showStoreItem)
        {

            ShowImageTemplate("storeItemOn", my_target.storeItemOn);
            ShowImageTemplate("storeItemOff", my_target.storeItemOff);
            ShowImageTemplate("storeItemSelected", my_target.storeItemSelected);

            my_target.storeItemCanBuyIco = EditorGUILayout.ObjectField("storeItemCanBuyIco", my_target.storeItemCanBuyIco, typeof(Sprite), true) as Sprite;
            my_target.storeItemCantBuyIco = EditorGUILayout.ObjectField("storeItemCantBuyIco", my_target.storeItemCantBuyIco, typeof(Sprite), true) as Sprite;

            
            ShowTextTemplate("storeItemName", my_target.storeItemName);
            ShowTextTemplate("storeItemPrice", my_target.storeItemPrice);
            ShowTextTemplate("storeItemQuantity", my_target.storeItemQuantity);
            ShowTextTemplate("storeItemBuyText", my_target.storeItemBuyText);
        }


        my_target.showProfileItem = EditorGUILayout.Foldout(my_target.showProfileItem, "showProfileItem");
        if (my_target.showProfileItem)
        {

            my_target.profileItemOn = EditorGUILayout.ObjectField("profileItemOn", my_target.profileItemOn, typeof(Sprite), true) as Sprite;
            my_target.profileItemOff = EditorGUILayout.ObjectField("profileItemOff", my_target.profileItemOff, typeof(Sprite), true) as Sprite;
            my_target.profileItemSelected = EditorGUILayout.ObjectField("profileItemSelected", my_target.profileItemSelected, typeof(Sprite), true) as Sprite;

            ShowTextTemplate("profileItemName", my_target.profileItemName);
            ShowTextTemplate("profileItemNumber", my_target.profileItemNumber);
            ShowTextTemplate("profileItemCount", my_target.profileItemCount);
            ShowTextTemplate("profileItemProgress", my_target.profileItemProgress);
        }




        my_target.showGameScene = EditorGUILayout.Foldout(my_target.showGameScene, "showGameScene");
        if (my_target.showGameScene)
        {

            EditorGUILayout.LabelField("starScoreProgressBar:");
                EditorGUI.indentLevel++;
                ShowImageTemplate("BK", my_target.starScoreProgressBarBK);
                ShowImageTemplate("Fill", my_target.starScoreProgressBarFill);
                EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("End screens emoticos:");
            EditorGUI.indentLevel++;
            for (int i = 0; i < Enum.GetNames(typeof(UI_Skin.EndScreenEmotion)).Length; i++)
                my_target.endScreenEmotions[i] = EditorGUILayout.ObjectField(Enum.GetName(typeof(UI_Skin.EndScreenEmotion), i), my_target.endScreenEmotions[i], typeof(Sprite), true) as Sprite;
            EditorGUI.indentLevel--;

            ShowImageTemplate("winScreenBK", my_target.winScreenBK);
            ShowImageTemplate("loseScreenBK", my_target.loseScreenBK);

            EditorGUILayout.LabelField("bigStar:");
                EditorGUI.indentLevel++;
                my_target.bigStarOn = EditorGUILayout.ObjectField("On", my_target.bigStarOn, typeof(Sprite), true) as Sprite;
                my_target.bigStarOff = EditorGUILayout.ObjectField("Off", my_target.bigStarOff, typeof(Sprite), true) as Sprite;
                EditorGUI.indentLevel--;


        }


        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(my_target);

    }


    void ShowImageTemplate(string myName, UI_Skin.ImageTemplate myImageTemplate)
    {
        EditorGUILayout.LabelField(myName);
        EditorGUI.indentLevel++;
        myImageTemplate.sprite = EditorGUILayout.ObjectField("sprite", myImageTemplate.sprite, typeof(Sprite), true) as Sprite;
        myImageTemplate.color = EditorGUILayout.ColorField("color", myImageTemplate.color);
        EditorGUI.indentLevel--;
    }

    void ShowTextTemplate(string myName, UI_Skin.TextTemplate myTextTemplate)
    {
        UI_Skin my_target = (UI_Skin)target;

        EditorGUILayout.LabelField(myName);
        EditorGUI.indentLevel++;

        //font
        myTextTemplate.myFontAsset = (UI_Skin.FontAsset)EditorGUILayout.EnumPopup("font", myTextTemplate.myFontAsset);
        EditorGUI.indentLevel++;
        if (myTextTemplate.myFontAsset == UI_Skin.FontAsset.Defalut)
            myTextTemplate.font = my_target.defaultFont;
        else if (myTextTemplate.myFontAsset == UI_Skin.FontAsset.Custom)
            myTextTemplate.font = EditorGUILayout.ObjectField("customFont", myTextTemplate.font, typeof(TMP_FontAsset), true) as TMP_FontAsset;
        EditorGUI.indentLevel--;

        //size
        myTextTemplate.myFontSize = (UI_Skin.FontSizes)EditorGUILayout.EnumPopup("size", myTextTemplate.myFontSize);
        EditorGUI.indentLevel++;
        if (myTextTemplate.myFontSize == UI_Skin.FontSizes.Custom)
            myTextTemplate.size = EditorGUILayout.IntField("customSize", myTextTemplate.size);
        else
            myTextTemplate.size = my_target.fontSizes[(int)myTextTemplate.myFontSize];
        EditorGUI.indentLevel--;

        //color
        myTextTemplate.myFontColor = (UI_Skin.FontColor)EditorGUILayout.EnumPopup("color", myTextTemplate.myFontColor);
        EditorGUI.indentLevel++;
        if (myTextTemplate.myFontColor == UI_Skin.FontColor.Custom)
            myTextTemplate.color = EditorGUILayout.ColorField("customColor", myTextTemplate.color);
        else
            myTextTemplate.color = my_target.defaultFontColor;
        EditorGUI.indentLevel--;


        EditorGUI.indentLevel--;
    }


}
