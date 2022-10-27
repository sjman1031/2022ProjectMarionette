using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[ExecuteInEditMode()]
public class UI_Template : MonoBehaviour {

    public UI_Skin currentUISkin;
    
    public virtual void Awake () {
        LoadUISkin();

    }

    public void Reskin(UI_Skin newSkin)
    {
        currentUISkin = newSkin;
        LoadUISkin();
    }

    protected virtual void LoadUISkin()
    {
        if (SkinMaster.THIS != null && SkinMaster.THIS.currentUISkin)
            currentUISkin = SkinMaster.THIS.currentUISkin;

    }

    public void UpdateText(TextMeshProUGUI targetText, UI_Skin.TextTemplate newText)
         {
        targetText.font = newText.font;
        targetText.fontSize = newText.size;
        targetText.color = newText.color;
        }

    public void UpdateImage(Image targetImage, UI_Skin.ImageTemplate newImage)
    {
        targetImage.sprite = newImage.sprite;
        targetImage.color = newImage.color;
    }


    //DEBUG ONLY!!! - remove from here...
    /*
    public virtual void Update()
    {
        if (Application.isEditor)
            LoadUISkin();
    }*/
    //... to here in order to improve performances



}
