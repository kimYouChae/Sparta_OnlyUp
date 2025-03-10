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
<<<<<<< Updated upstream

=======
    [SerializeField] private PlayerInteration interaction;
    [SerializeField] private PlayerUseItem useItem;
    
>>>>>>> Stashed changes
    [Header("=== State ===")]
    [SerializeField] private float speed;
    [SerializeField] private int jumpCount;

    [Header("===Component===")]
    [SerializeField] private Transform dropItemTrs;
    [SerializeField] private Transform weaponEquipTrs;

    public PlayerMovement PlayerMovement { get => movement; }
    public PlayerAnimator PlayerAnimator { get => animator; }
<<<<<<< Updated upstream
=======
    public PlayerInteration PlayerInteraction { get => interaction; }
    public PlayerUseItem PlayerUseItem { get => useItem; }
    public Transform WeaponEquipTrs { get => weaponEquipTrs; }
    public Transform DropItemTrs { get => dropItemTrs;  }

    // ºˆ¡§øπ¡§ 
>>>>>>> Stashed changes
    public float Speed { get => speed; }
    public int JumpCount { get => jumpCount; }

    private void Awake()
    {
        instance = this;
    }

    public void UpdatePlayerState(PlayerState state, float value) 
    {
        switch (state) 
        { 
            case PlayerState.HP:
                break;
            case PlayerState.Speed:
                speed += value;
                break;
            case PlayerState.JumpCount:
                jumpCount += (int)value;
                break;
        }
    }
}
