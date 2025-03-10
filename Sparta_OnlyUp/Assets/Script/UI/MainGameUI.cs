using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [Header("===Text==")]
    [SerializeField] private TextMeshProUGUI interactText;

    [Header("===Item Info===")]
    private Item currItem;

    [Header("===Buff Info===")]
    [SerializeField] private GameObject buffImagePrefab;
    [SerializeField] private Sprite[] buffIcon;
    [SerializeField] private Transform buffParent;

    private void Start()
    {
        interactText.text = "";
    }

    public void InteractItem(int idx) 
    {
        // idx ; 현재 아이템에 해당하는 인덱스
        currItem = MainGameManager.Instance.itemManager.ReturnItem(idx);
        
        if (currItem == null)
        {
            interactText.text = "";
            return;
        }

        interactText.text = "[E] \n " + currItem.ItemName + "\n" + currItem.ItemToopTip;
    }

    public void UpdateBuffIcon( int idx , bool flag = true  )   // true이면 추가, false이면 삭제 
    {
        // 버프 인덱스 (7번 , 8번 ) 보정
        int currIdx = idx - MainGameManager.Instance.itemManager.WeaponCount;

        // 이미지 추가 
        if (flag)
        {
            GameObject bu = Instantiate(buffImagePrefab);
            bu.transform.SetParent(buffParent , false);

            try
            {
                bu.GetComponent<Image>().sprite = buffIcon[currIdx - 1];
            }
            catch (Exception e) { Debug.Log($"MainGameUi : 버프 이미지 설정 중 오류 발생 {e}"); }
        }
        // 이미지 삭제
        else 
        {
            try 
            {
                Destroy(buffParent.GetChild(0));
            }
            catch (Exception e) { Debug.Log($"MainGameUi : 버프 이미지 삭제 중 오류 발생 {e}"); }
        }
    }


}
