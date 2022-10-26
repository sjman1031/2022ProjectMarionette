using UnityEngine;

[CreateAssetMenu(menuName = "MenuKit/itemDatabase/Consumable item")]
public class ConsumableItem : Item
{
    [HideInInspector] public Kind kind = Kind.Consumable;
}
