using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItem : MonoBehaviour
{
    [Header("===Items===")]
    [SerializeField] private Weapon currWeaponItem;             // 현재 무기 아이템
    [SerializeField] private List<UsableItem> currUsableItems;  // 사용가능 아이템 리스트
    [SerializeField] private GameObject currWeapon;

    private void Start()
    {
        currUsableItems = new List<UsableItem>();
        currWeaponItem = null;
    }

    public void ObtainItem(GameObject currItem) 
    {
        int index = currItem.GetComponent<ItemCrops>().ItemNum;
        Item it = MainGameManager.Instance.itemManager.ReturnItem(index);

        if (it.ItemType == ItemType.weapon)
        {
            ObtainWeapon(currItem , it);
            
            // 현재 아이템으로
            currWeaponItem = (Weapon)it;
        }
        else 
        {
            // 리스트에 추가 
            currUsableItems.Add((UsableItem)it);
        }
    }

    private void ObtainWeapon(GameObject obj , Item wa) 
    {
        // 이미 장착하고있으면 
        if(currWeapon != null) 
        {
            DropItem(currWeapon);
        }

        // 장착
        EquipItem(obj);

        // 현재 아이템으로
        currWeapon = obj;
    }

    private void DropItem(GameObject curr) 
    {
        // 아이템 부모 하위로 
        curr.transform.SetParent(MainGameManager.Instance.itemParent);

        // 
        curr.AddComponent<Rigidbody>();

        // 상호작용 레이어로 변경 
        curr.layer = LayerManager.Instance.InteractiveLayerInt;
    }

    private void EquipItem(GameObject curr) 
    {
        // 획득 위치로 
        curr.transform.SetParent(PlayerManager.Instance.WeaponEquipTrs);
        curr.transform.localPosition = Vector3.zero;

        // 중력 미적용 
        Destroy(curr.GetComponent<Rigidbody>());

        // 획득 레이어로 변경 
        curr.layer = LayerManager.Instance.OwnItemLayerInt;
    }

    
}
