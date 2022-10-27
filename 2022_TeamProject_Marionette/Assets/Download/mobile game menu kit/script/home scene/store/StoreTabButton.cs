using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreTabButton : MonoBehaviour {

    public TextMeshProUGUI myText;
    public Image myImage;

    int myId;
    store_tabs my_store_tabs;

    public void StartMe(int id, store_tabs store, string myName, Sprite myIcon)
    {
        myId = id;
        my_store_tabs = store;

        if (myName == "")
            myText.gameObject.SetActive(false);
        else
            myText.text = myName;

        if (myIcon == null)
            myImage.gameObject.SetActive(false);
        else
            myImage.sprite = myIcon;
    }

    public void ClickMe()
    {
        my_store_tabs.Select_this_tab(myId);
    }
}
