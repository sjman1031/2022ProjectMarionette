using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase THIS;
    public ConsumableItem[] consumableItems;
    public NonConsumableItem[] nonConsumableItems;
    public IncrementalItem[] incrementalItems;

    private void Awake()
    {
        THIS = this;
    }

    public int GetConsumableItemSlot(ConsumableItem item)
    {
        for (int i = 0; i < consumableItems.Length; i++)
        {
            if (consumableItems[i] == item)
                return i;
        }

        return -1;
    }


    public int GetNonConsumableItemSlot(NonConsumableItem item)
    {
        for (int i = 0; i < nonConsumableItems.Length; i++)
        {
            if (nonConsumableItems[i] == item)
                return i;
        }

        return -1;
    }

    public int GetIncrementalItemSlot(IncrementalItem item)
    {
        for (int i = 0; i < incrementalItems.Length; i++)
        {
            if (incrementalItems[i] == item)
                return i;
        }

        return -1;
    }
}
