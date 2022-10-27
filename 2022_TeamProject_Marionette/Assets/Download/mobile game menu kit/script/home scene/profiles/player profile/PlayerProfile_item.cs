using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class PlayerProfile {
    
    public Sprite GetItemIco(Item.Kind kind, int id)
    {
        if (kind == Item.Kind.Consumable)
            return GetConsumableItem(id).item.icon;
        else if (kind == Item.Kind.NonConsumable)
            return GetNonConsumableItem(id).item.icon;
        else if (kind == Item.Kind.Incremental)
            return GetIncrementalItem(id).item.itemLeves[GetIncrementalItem(id).level].icon;

        return null;
    }

    public string GetItemName(Item.Kind kind, int id)
    {
        if (kind == Item.Kind.Consumable)
            return GetConsumableItem(id).item.myName;
        else if (kind == Item.Kind.NonConsumable)
            return GetNonConsumableItem(id).item.myName;
        else if (kind == Item.Kind.Incremental)
            return GetIncrementalItem(id).item.itemLeves[GetIncrementalItem(id).level].myName;

        return null;
    }

    public string GetItemDescription(Item.Kind kind, int id)
    {
        if (kind == Item.Kind.Consumable)
            return GetConsumableItem(id).item.description;
        else if (kind == Item.Kind.NonConsumable)
            return GetNonConsumableItem(id).item.description;
        else if (kind == Item.Kind.Incremental)
            return GetIncrementalItem(id).item.itemLeves[GetIncrementalItem(id).level].description;

        return null;
    }
}
