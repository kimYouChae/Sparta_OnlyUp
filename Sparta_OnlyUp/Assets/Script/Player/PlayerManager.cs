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

    [Header("=== State ===")]
    [SerializeField] private float speed;
    [SerializeField] private int jumpCount;

    public PlayerMovement PlayerMovement { get => movement; }
    public PlayerAnimator PlayerAnimator { get => animator; }
    public PlayerInteration PlayerInteraction { get => interaction; }
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
