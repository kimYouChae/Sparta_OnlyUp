using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public PlayerMovement PlayerMovement { get => movement; }
    public PlayerAnimator PlayerAnimator { get => animator; }

    private void Awake()
    {
        instance = this;
    }
}
