using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "MenuKit/UI Skin")]
public class UI_Skin : ScriptableObject
{
    [System.Serializable]
    public class TextTemplate
    {
        public FontAsset myFontAsset;
        public TMP_FontAsset font;

        public FontSizes myFontSize;
        public int size;

        public FontColor myFontColor;
        public Color color = Color.white;
    }

    [System.Serializable]
    public class ImageTemplate
    {
        public Sprite sprite;
        public Color color = Color.white;
    }

    public enum ButtonStatus
    {
        Off,
        On,
        Selected
    }

public enum EndScreenEmotion
    {
        Happy,
        Normal,
        Sad
    }

    //editor
    public bool showIcons;
    public enum Icon
    {
        Start,
        Next,
        Previous,
        Back,

        Credit,
        ScoreRank,
        Achievement,
        NoWiFi,
        Profiles,
        Store,

        Options,
        SoundOn,
        SoundOff,

        Plus,
        Minus,

        StarOn,
        StarOff,

        Lives,
        VirtualMoney,
        RealMoney,

        PadlockSmall,
        PadlockBig,

        GarbageCan,

        Pause,
        PlayVideoAds,
        Retry,
        BackToStageScreen,

        ContinueToken,
        UnlockWorld,

    }
    public Sprite[] icons = new Sprite[1];

    public bool showText;
    public enum FontSizes
    {
        Small,
        Medium,
        Big,
        Huge,
        Custom
    }
    public enum FontColor
    {
        Defalut,
        Custom
    }
    public enum FontAsset
    {
        Defalut,
        Custom
    }

    [Header("Text")]
    public TMP_FontAsset defaultFont;
    public Color defaultFontColor = Color.white;
    public int[] fontSizes = new int[] {24,32,64,128};
    public TextTemplate[] defaultTexts = new TextTemplate[4];

    [Header("Window")]
    public bool showWindow;
        public ImageTemplate windowBK;

        public TextTemplate windowHeaderText;
        public TextTemplate windowText;
        public TextTemplate windowBigText;

        public ImageTemplate windowScrollbarBK;
        public ImageTemplate windowScrollbarHandle;


    [Space()]
    [Header("Input Field")]
    public bool showInputField;
    public ImageTemplate inputFieldBK;
    public TextTemplate inputFieldText;


    [Space()]
    [Header("Screen")]
    public bool showScreen;
    public ImageTemplate screenBK;
    public TextTemplate screenHeaderText;

    [Space()]
    [Header("Button")]
    public bool showButton;
    public ImageTemplate buttonSprite;
    public ImageTemplate buttonSpriteSelected;
    public ImageTemplate buttonSpriteOff;
    public TextTemplate buttonText;
    public TextTemplate buttonCountText;


    [Space()]
    [Header("Option Sound Element")]
    public bool showOptionSoundElement;
    public ImageTemplate optionSoundElementBK;
    public ImageTemplate optionSoundElementSliderBK;
    public ImageTemplate optionSoundElementSliderFill;
    public ImageTemplate optionSoundElementSliderHandle;
    public TextTemplate optionSoundElementText;


    [Space()]
    [Header("Info bar")]
    public bool showInfoBar;
    public ImageTemplate infoBarBK;
    public ImageTemplate infoBarTimerBK;
    public TextTemplate infoBarText;

    [Space()]
    [Header("Stage Button")]
    public bool showStageButton;
    public Sprite stageButtonOn;
    public Sprite stageButtonOff;
    public Sprite stageButtonNextStageToPlay;
    public Sprite stageButtonTailDotOn;
    public Sprite stageButtonTailDotOff;
    public TextTemplate stageButtonNumber;
    public TextTemplate stageButtonStarCount;

    [Space()]
    [Header("World Button")]
    public int totalWorlds;
    public bool showWorls;
    public Sprite[] worldButtonIcons = new Sprite[1];
    public TextTemplate worldButtonName;
    public TextTemplate worldButtonNumber;
    public TextTemplate worldButtonStarsNeed;

    [Space()]
    [Header("World menu backgrounds")]
    public Sprite[] worldBackgrounds = new Sprite[1];

    [Space()]
    [Header("Page Dot")]
    public bool showPageDot;
    public ImageTemplate pageDotON;
    public ImageTemplate pageDotOFF;

    [Space()]
    [Header("Score record screen element")]
    public bool showScoreRecordScreenElement;
    public ImageTemplate scoreScreenElementBK;
    public TextTemplate scoreRank;
    public TextTemplate scoreName;
    public TextTemplate scoreRecord;

    [Space()]
    [Header("Store item")]
    public bool showStoreItem;
    public ImageTemplate storeItemOn;
    public ImageTemplate storeItemOff;
    public ImageTemplate storeItemSelected;
    public Sprite storeItemCanBuyIco;
    public Sprite storeItemCantBuyIco;
    public TextTemplate storeItemName;
    public TextTemplate storeItemPrice;
    public TextTemplate storeItemQuantity;
    public TextTemplate storeItemBuyText;


    [Space()]
    [Header("Profile item")]
    public bool showProfileItem;
    public Sprite profileItemOn;
    public Sprite profileItemOff;
    public Sprite profileItemSelected;
    public TextTemplate profileItemName;
    public TextTemplate profileItemNumber;
    public TextTemplate profileItemCount;
    public TextTemplate profileItemProgress;

    [Space()]
    [Header("GameScene")]
    public bool showGameScene;
    public ImageTemplate starScoreProgressBarBK;
    public ImageTemplate starScoreProgressBarFill;
    public Sprite[] endScreenEmotions = new Sprite[3];
    public ImageTemplate winScreenBK;
    public ImageTemplate loseScreenBK;
    public Sprite bigStarOn;
    public Sprite bigStarOff;


    public Sprite GetEmoticon(EndScreenEmotion emo)
    {
        return endScreenEmotions[(int)emo];
    }

    public Sprite GetIcon(Icon thisIcon)
    {
        return icons[(int)thisIcon];
    }

}
