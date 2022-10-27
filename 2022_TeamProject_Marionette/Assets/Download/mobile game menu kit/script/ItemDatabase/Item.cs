using UnityEngine;

public class Item : ScriptableObject
{
    public string myName;
    public Sprite icon;
    [TextArea()]
    public string description;
    public enum Kind
    {
        Consumable,
        NonConsumable,
        Incremental
    }

}
