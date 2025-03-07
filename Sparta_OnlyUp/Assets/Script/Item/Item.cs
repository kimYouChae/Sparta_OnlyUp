
using UnityEngine;
using System;

public enum ItemType 
{
    weapon,
    useable
}

[System.Serializable]
public class ItemWrapper : ICsvParsable
{
    [SerializeField] private int itemNum;
    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;
    [SerializeField] private string itemToopTip;
    [SerializeField] float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float durationTime;

    public int ItemNum { get => itemNum;  }
    public ItemType ItemType { get => itemType; }
    public string ItemName { get => itemName; }
    public string ItemToopTip { get => itemToopTip; }
    public float AttackSpeed { get => attackSpeed;  }
    public float AttackDamage { get => attackDamage; }
    public float DurationTime { get => durationTime;  }

    public void Parse(string[] values)
    {
        // [0] item num
        // [1] item type
        // [2] name
        // [3] tooltip
        // [4] attackSpeed
        // [5] attackDamage
        // [6] durationTime

        itemNum         = int.Parse(values[0]);
        itemType        = (ItemType)Enum.Parse(typeof(ItemType), values[1]);
        itemName        = values[2];
        itemToopTip     = values[3];
        attackSpeed     = float.Parse(values[4]);
        attackDamage    = float.Parse(values[5]);
        durationTime    = float.Parse(values[6]);

    }
}

