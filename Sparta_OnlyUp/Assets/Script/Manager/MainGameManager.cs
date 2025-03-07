using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    private static MainGameManager instance;
    public static MainGameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
            {
                GameObject gameObject = new GameObject("PlayerManger");
                instance = gameObject.AddComponent<MainGameManager>();
                return instance;
            }
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    [Header("===Script===")]
    public MainGameUI mainGameUi;
    public ItemManager itemManager;
    public PlayerManager playerManager;
}
