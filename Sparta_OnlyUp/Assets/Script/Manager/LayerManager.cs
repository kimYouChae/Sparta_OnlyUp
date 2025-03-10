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
    [SerializeField] private LayerMask interativeLayer;
    [SerializeField] private LayerMask ownItemLayer;

    [Header("===Layer Int===")]
    [SerializeField] private int walkableLayerInt;
    [SerializeField] private int playerLayerInt;
    [SerializeField] private int interactiveLayerInt;
    [SerializeField] private int ownItemLayerInt;

    public LayerMask WalkableLayer { get => walkableLayer;  }
    public int WalkableLayerInt { get => walkableLayerInt;  }
    public LayerMask IgnorePlayerLayer { get => ignorePlayerLayer; }
    public LayerMask InterativeLayer { get => interativeLayer;  }
    public int InteractiveLayerInt { get => interactiveLayerInt; }
    public LayerMask OwnItemLayer { get => ownItemLayer; set => ownItemLayer = value; }
    public int OwnItemLayerInt { get => ownItemLayerInt; set => ownItemLayerInt = value; }
    public LayerMask PlayerLayer { get => playerLayer;  }

    // Start is called before the first frame update
    void Start()
    {
        walkableLayer       = LayerMask.GetMask("Walkable");
        playerLayer         = LayerMask.GetMask("Player");
        interativeLayer     = LayerMask.GetMask("InterativeItem");
        ownItemLayer        = LayerMask.GetMask("OwnItem");

        walkableLayerInt    = LayerMask.NameToLayer("Walkable");
        playerLayerInt      = LayerMask.NameToLayer("Player");
        interactiveLayerInt = LayerMask.NameToLayer("InterativeItem");
        ownItemLayerInt     = LayerMask.NameToLayer("OwnItem");
    }


}
