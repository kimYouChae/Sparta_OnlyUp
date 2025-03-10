using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    string wrapperName = "ItemWrapper";

    [Header("===Item Container===")]
    private List<Item> items;
    private Dictionary<int, Weapon> weaponContainer;
    private Dictionary<int, UsableItem> usableItemContainer;

    [Header("===ItemPrefabs===")]
    [SerializeField] List<GameObject> itemPrefabs;

    public List<GameObject> ItemPrefabs { get => itemPrefabs; }

    public int WeaponCount => weaponContainer.Count;

    void Start()
    {
        InitItem();
    }

    private void InitItem() 
    {
        // 역직렬화 
        List<ItemWrapper> wrapper = JsonSerialized.Deserialization<ItemWrapper>(wrapperName);

        items = new List<Item>();
        weaponContainer = new Dictionary<int, Weapon>();
        usableItemContainer = new Dictionary<int, UsableItem>();

        foreach (ItemWrapper itemWrapper in wrapper)
        {
            // weapon이면 ?
            if (itemWrapper.ItemType == ItemType.weapon)
            {
                Weapon weapon = new Weapon
                    (
                        itemWrapper.ItemNum,
                        itemWrapper.ItemType,
                        itemWrapper.ItemName,
                        itemWrapper.ItemToopTip,
                        itemWrapper.AttackSpeed,
                        itemWrapper.AttackDamage
                    );

                weaponContainer.Add(itemWrapper.ItemNum, weapon);
                items.Add(weapon);
            }
            // usable 이면 ?
            else
            {
                UsableItem usable = new UsableItem
                    (
                        itemWrapper.ItemNum,
                        itemWrapper.ItemType,
                        itemWrapper.ItemName,
                        itemWrapper.ItemToopTip,
                        itemWrapper.DurationTime,
                        itemWrapper.PlayerState
                    );

                usableItemContainer.Add(itemWrapper.ItemNum, usable);
                items.Add(usable);
            }
        }
    }

    public Item ReturnItem( int idx ) 
    {
        // 아이템은 1 ~ 8번 

        if (idx < 0)
            return null;
        if (idx > items.Count)
            return null;

        try 
        {
            return items[idx - 1];
        }
        catch(Exception e)
        {
            Debug.Log($"ItemManger : 인덱스에 해당하는 아이템 리턴중 오류 : {e}");
            return null;
        }
    }

}
