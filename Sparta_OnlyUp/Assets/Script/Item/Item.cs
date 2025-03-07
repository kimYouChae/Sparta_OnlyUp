
using UnityEngine;
using System;

public enum ItemType 
{
    weapon,
    useable
}


public class Item
{
    [SerializeField] private int itemNum;
    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;
    [SerializeField] private string itemToopTip;

    public Item(int num , ItemType type, string name, string tool)
    {
        this.itemNum = num;
        this.itemType = type;
        this.itemName = name;
        this.itemToopTip = tool;
    }
}

public class Weapon : Item
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;

    public Weapon(int num, ItemType type, string name, string tool , float attck, float damage) : base(num, type , name, tool)
    {
        this.attackSpeed = attck;
        this.attackDamage = damage;
    }
}

public class UsableItem : Item 
{
    [SerializeField] private float durationTime;
    [SerializeField] private PlayerState playerState;

    public UsableItem(int num, ItemType type, string name, string tool, float dura , PlayerState state) : base(num, type, name, tool)
    { 
        this.durationTime = dura;
        this.playerState = state;
    }
}

[System.Serializable]
public class ItemWrapper : ICsvParsable
{
    [SerializeField] private int itemNum;
    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;
    [SerializeField] private string itemToopTip;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float durationTime;
    [SerializeField] private PlayerState playerState;

    public int ItemNum { get => itemNum;  }
    public ItemType ItemType { get => itemType; }
    public string ItemName { get => itemName; }
    public string ItemToopTip { get => itemToopTip; }
    public float AttackSpeed { get => attackSpeed;  }
    public float AttackDamage { get => attackDamage; }
    public float DurationTime { get => durationTime;  }
    public PlayerState PlayerState { get => playerState; }

    public void Parse(string[] values)
    {
        // [0] item num
        // [1] item type
        // [2] name
        // [3] tooltip
        // [4] attackSpeed
        // [5] attackDamage
        // [6] durationTime
        // [7] PlayerState

        itemNum         = int.Parse(values[0]);
        itemType        = (ItemType)Enum.Parse(typeof(ItemType), values[1]);
        itemName        = values[2];
        itemToopTip     = values[3];
        attackSpeed     = float.Parse(values[4]);
        attackDamage    = float.Parse(values[5]);
        durationTime    = float.Parse(values[6]);
        playerState     = (PlayerState)Enum.Parse(typeof(PlayerState) , values[7]);
    }
}

