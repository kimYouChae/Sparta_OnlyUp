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
        
        // 아이템 획득 
        if (it.ItemType == ItemType.weapon)
        {
            ObtainWeapon(currItem, it);

            currWeaponItem = (Weapon)it;
        }
        // 버프 획득 
        else
        {
            currUsableItems.Add((UsableItem)it);

            // Ui 업데이트
            MainGameManager.Instance.mainGameUi.UpdateBuffIcon(idx);
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

    public void UseBuff() 
    {
        if (currUsableItems.Count <= 0)
            return;

        // usable 아이템 맨 앞에 있는거 사용 
        UsableItem currItem = currUsableItems[0];

        // Ui 업데이트
        MainGameManager.Instance.mainGameUi.UpdateBuffIcon(currItem.ItemNum, false);

        int amout = currItem.PlayerState == PlayerState.Speed ? 3 : 1;

        StartCoroutine( UseBuff(currItem.PlayerState , amout , currItem.DurationTime));
    }

    IEnumerator UseBuff(PlayerState state, int amount , float time) 
    {
        // 원래 플레이어 스탯
        int ori = PlayerManager.Instance.ReturnPlayerState(state);

        // 플레이어 스탯 업데이트 
        PlayerManager.Instance.UpdatePlayerState(state , amount);

        yield return new WaitForSeconds(time);

        PlayerManager.Instance.UpdatePlayerState(state, -ori);

        // 리스트에 있는 첫번째거 지우기
        DeleteBuffItem();

    }

    private void DeleteBuffItem() 
    {
        currUsableItems.RemoveAt(0);
    }
}
