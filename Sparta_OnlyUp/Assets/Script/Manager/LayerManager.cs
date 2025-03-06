using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    // 싱글톤
    private static LayerManager instance;
    public static LayerManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
            {
                GameObject gameObject = new GameObject("LayerManager");
                instance = gameObject.AddComponent<LayerManager>();
                return instance;
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }
     
    [Header("===Inspector===")]
    [SerializeField] private LayerMask ignorePlayerLayer;   // 플레이어 레이어를 제외한 레이어

    [Header("===Layermask===")]
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("===Layer Int===")]
    [SerializeField] private int walkableLayerInt;
    [SerializeField] private int playerLayerInt;
    
    public LayerMask WalkableLayer { get => walkableLayer;  }
    public int WalkableLayerInt { get => walkableLayerInt;  }
    public LayerMask IgnorePlayerLayer { get => ignorePlayerLayer; }

    // Start is called before the first frame update
    void Start()
    {
        walkableLayer = LayerMask.GetMask("Walkable");
        playerLayer = LayerMask.GetMask("Player");

        walkableLayerInt = LayerMask.NameToLayer("Walkable");
        playerLayerInt = LayerMask.NameToLayer("Player");
    }


}
