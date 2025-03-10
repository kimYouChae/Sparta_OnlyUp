using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState 
{
    HP,
    Speed,
    JumpCount
}

public class PlayerManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get 
        {
            if (instance != null)
                return instance;
            else
            {
                GameObject gameObject = new GameObject("PlayerManger");
                instance = gameObject.AddComponent<PlayerManager>();
                return instance;
            }    
        }
    }

    [Header("===Script===")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAnimator animator;
    [SerializeField] private PlayerInteration interaction;
    [SerializeField] private PlayerUseItem useItem;

    [Header("=== State ===")]
    [SerializeField] private int speed;
    [SerializeField] private int jumpCount;

    [Header("===Component===")]
    [SerializeField] private Transform dropItemTrs;
    [SerializeField] private Transform weaponEquipTrs;

    public PlayerMovement PlayerMovement { get => movement; }
    public PlayerAnimator PlayerAnimator { get => animator; }
    public PlayerInteration PlayerInteraction { get => interaction; }
    public PlayerUseItem PlayerUseItem { get => useItem; }
    
    public Transform WeaponEquipTrs { get => weaponEquipTrs; }
    public Transform DropItemTrs { get => dropItemTrs; }

    public int Speed { get => speed; }
    public int JumpCount { get => jumpCount; }

    private void Awake()
    {
        instance = this;

        speed = 3;
        jumpCount = 1;
    }

    public void UpdatePlayerState(PlayerState state, int value) 
    {
        switch (state) 
        { 
            case PlayerState.HP:
                break;
            case PlayerState.Speed:
                speed += value;
                break;
            case PlayerState.JumpCount:
                jumpCount += value;
                break;
        }
    }

    public int ReturnPlayerState( PlayerState state) 
    {
        switch (state)
        {
            case PlayerState.HP:
                return -1;
            case PlayerState.Speed:
                return speed;
            case PlayerState.JumpCount:
                return jumpCount;
        }
        return -1;
    }
}
