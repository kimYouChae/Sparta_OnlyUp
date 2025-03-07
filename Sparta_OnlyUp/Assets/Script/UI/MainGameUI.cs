using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [Header("===Text==")]
    [SerializeField] private TextMeshProUGUI interactText;

    private Item currItem;

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

}
