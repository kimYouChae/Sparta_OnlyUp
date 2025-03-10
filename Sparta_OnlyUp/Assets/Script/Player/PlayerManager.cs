using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState 
{
    HP,
    Speed,
    JumpCount
}

[System.Serializable]
public class Player 
{
    [Header("=== State ===")]
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int speed;
    [SerializeField] private int jumpCount;

    public Player(int h , int mx, int s , int jc) 
    {
        this.hp = h;
        this.maxHp = mx;
        this.speed = s;
        this.jumpCount = jc;
    }

    public int Hp { get => hp; set { hp = value; } }
    public int MaxHp { get => maxHp; set { maxHp = value; }  }

    public int Speed { get => speed; set { speed = value; } }
    public int JumpCount { get => jumpCount; set { jumpCount = value; } }
}

public class PlayerManager : MonoBehaviour
{
    // ½Ì±ÛÅæ
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

    [Header("===Component===")]
    [SerializeField] private Transform dropItemTrs;
    [SerializeField] private Transform weaponEquipTrs;

    [Header("===State===")]
    [SerializeField] private Player player;

    public PlayerMovement PlayerMovement { get => movement; }
    public PlayerAnimator PlayerAnimator { get => animator; }
    public PlayerInteration PlayerInteraction { get => interaction; }
    public PlayerUseItem PlayerUseItem { get => useItem; }
    
    public Transform WeaponEquipTrs { get => weaponEquipTrs; }
    public Transform DropItemTrs { get => dropItemTrs; }

    public int Speed { get => player.Speed; }
    public int JumpCount { get => player.JumpCount; }

    private void Awake()
    {
        instance = this;

        player = new Player(50, 50, 3,1);
    }

    private void Start()
    {
        StartCoroutine(ReduceState());
    }

    public void UpdatePlayerState(PlayerState state, int value) 
    {
        switch (state) 
        { 
            case PlayerState.HP:
                player.Hp += value;
                break;
            case PlayerState.Speed:
                player.Speed += value;
                break;
            case PlayerState.JumpCount:
                player.JumpCount += value;
                break;
        }
    }

    public int ReturnPlayerState( PlayerState state) 
    {
        switch (state)
        {
            case PlayerState.HP:
                return player.Hp;
            case PlayerState.Speed:
                return player.Speed;
            case PlayerState.JumpCount:
                return player.JumpCount;
        }
        return -1;
    }

    IEnumerator ReduceState() 
    {
        while (player.Hp > 0) 
        {
            player.Hp--;

            float temp = player.Hp / (float)player.MaxHp;

            // UI ¾÷µ¥ÀÌÆ® 
            MainGameManager.Instance.mainGameUi.UpdateHpBar(temp);

            yield return new WaitForSeconds(3f);
        }
    }
}
