using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    string wrapperName = "ItemWrapper";
    private Dictionary<int, Weapon> weaponContainer;
    private Dictionary<int, UsableItem> usableItemContainer;

    void Start()
    {
        InitItem();
    }

    private void InitItem() 
    {
        // 역직렬화 
        List<ItemWrapper> wrapper = JsonSerialized.Deserialization<ItemWrapper>(wrapperName);

        weaponContainer = new Dictionary<int, Weapon>();
        usableItemContainer = new Dictionary<int, UsableItem>();

        foreach (ItemWrapper itemWrapper in wrapper)
        {
            // weapon이면 ?
            if (itemWrapper.ItemType == ItemType.weapon)
            {
                weaponContainer.Add(itemWrapper.ItemNum, new Weapon
                    (
                        itemWrapper.ItemNum,
                        itemWrapper.ItemType,
                        itemWrapper.ItemName,
                        itemWrapper.ItemToopTip,
                        itemWrapper.AttackSpeed,
                        itemWrapper.AttackDamage
                    ));
            }
            // usable 이면 ?
            else
            {
                usableItemContainer.Add(itemWrapper.ItemNum, new UsableItem
                    (
                        itemWrapper.ItemNum,
                        itemWrapper.ItemType,
                        itemWrapper.ItemName,
                        itemWrapper.ItemToopTip,
                        itemWrapper.DurationTime,
                        itemWrapper.PlayerState
                    ));
            }
        }
    }

    public Item ReturnItem( int idx ) 
    {
        if (idx <= 0) return null;
        else if (idx < weaponContainer.Count)
            return weaponContainer[idx];
        else if (idx < usableItemContainer.Count)
            return usableItemContainer[idx];
        else return null;
    }

}
