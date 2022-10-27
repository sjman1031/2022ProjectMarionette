using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MenuKit/itemDatabase/Incremental item")]
public class IncrementalItem : ScriptableObject
{
    [HideInInspector] public Item.Kind kind = Item.Kind.Incremental;
    public NonConsumableItem[] itemLeves;


}
