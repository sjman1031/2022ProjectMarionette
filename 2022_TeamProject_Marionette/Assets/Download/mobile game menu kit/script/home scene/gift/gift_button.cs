using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class gift_button : MonoBehaviour {

    /*
	[HideInInspector]public ConsumableItem give_this_selected;
	[HideInInspector]public int item_id;
    [HideInInspector] public Sprite consumableItemIcon;
	*/

	public TextMeshProUGUI quantity_text;
    public Toggle myToggle;

    public Image selectionImage;
	public Image my_ico;
    int id;
    string myName;
    string myDescription;

    public gift_manager my_gift_manager;

	

	public void Start_me (int _id, Sprite _icon, string _name, string _description, int _quantity) {

			id = _id;
            my_ico.sprite = _icon;
            myName = _name;
            myDescription = _description;

            this.gameObject.SetActive(true);


			//decide if you need to show quantity
			if (_quantity > 1)
				{
				quantity_text.gameObject.SetActive(true);
				quantity_text.text = _quantity.ToString("N0");
				}
			else
				quantity_text.gameObject.SetActive(false);


	}

    public void SelectMe()
    {
        myToggle.isOn = true;
        my_gift_manager.Select_this_button(id,myName,myDescription);
        selectionImage.sprite = SkinMaster.THIS.currentUISkin.profileItemSelected;
    }

}
