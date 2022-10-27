using UnityEngine;

[CreateAssetMenu(menuName = "MenuKit/itemDatabase/NonConsumable item")]
public class NonConsumableItem : Item
{
    [HideInInspector] public Kind kind = Kind.NonConsumable;
}
