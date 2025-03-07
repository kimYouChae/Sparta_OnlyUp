using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrops : MonoBehaviour
{
    [SerializeField]
    private int itemNum;

    public int ItemNum { get => itemNum; set => itemNum = value; }
}
