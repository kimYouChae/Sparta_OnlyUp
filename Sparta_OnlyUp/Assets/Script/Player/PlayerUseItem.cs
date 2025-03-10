using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItem : MonoBehaviour
{
    [Header("===Items===")]
    [SerializeField] private Weapon currWeaponItem;             
    [SerializeField] private List<UsableItem> currUsableItems;  
    [SerializeField] private GameObject currWeapon;

    private void Start()
    {
        currUsableItems = new List<UsableItem>();
        currWeaponItem = null;
    }

    public void ObtainItem(GameObject currItem , int idx)
    {
        int index = currItem.GetComponent<ItemCrops>().ItemNum;
        Item it = MainGameManager.Instance.itemManager.ReturnItem(index);

        if (it.ItemType == ItemType.weapon)
        {
            ObtainWeapon(currItem, it);

            currWeaponItem = (Weapon)it;
        }
        else
        {
            currUsableItems.Add((UsableItem)it);
        }
    }

    private void ObtainWeapon(GameObject obj, Item wa)
    {
        if (currWeapon != null)
        {
            DropItem(currWeapon);
        }

        EquipItem(obj);

        currWeapon = obj;
    }

    private void DropItem(GameObject curr)
    {
        curr.transform.SetParent(MainGameManager.Instance.itemParent);

        // 
        curr.AddComponent<Rigidbody>();

        curr.layer = LayerManager.Instance.InteractiveLayerInt;
    }

    private void EquipItem(GameObject curr)
    {
        curr.transform.SetParent(PlayerManager.Instance.WeaponEquipTrs);
        curr.transform.localPosition = Vector3.zero;

        Destroy(curr.GetComponent<Rigidbody>());

        curr.layer = LayerManager.Instance.OwnItemLayerInt;
    }
}
