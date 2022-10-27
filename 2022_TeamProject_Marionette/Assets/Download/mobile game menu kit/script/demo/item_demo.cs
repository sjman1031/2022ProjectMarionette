using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class item_demo : MonoBehaviour {

	public Image my_ico;
	public TextMeshProUGUI my_quantity;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    string itemName;
    string itemDescrition;
    int itemQuantity;
    Sprite itemIco;

	game_master my_game_master;
    Item.Kind itemKind;
    int itemId;


    public void StartMe(game_master _my_game_master, Item.Kind _itemKind, int _itemId)
    {
        my_game_master = _my_game_master;
        itemKind = _itemKind;
        itemId = _itemId;

        itemName = _my_game_master.GetCurrentProfile().GetItemName(_itemKind, _itemId);
        itemDescrition = _my_game_master.GetCurrentProfile().GetItemDescription(_itemKind, _itemId);
        itemIco = _my_game_master.GetCurrentProfile().GetItemIco(_itemKind, _itemId);

        nameText.text = itemName;
        descriptionText.text = itemDescrition;
        my_ico.sprite = itemIco;

        itemQuantity = 0;
        if (_itemKind == Item.Kind.Consumable)
            itemQuantity = _my_game_master.GetCurrentProfile().GetConsumableItemQuantity(_itemId);

        my_quantity.text = itemQuantity.ToString("n0");
        my_quantity.gameObject.SetActive(itemQuantity > 1);
        
    }
}
