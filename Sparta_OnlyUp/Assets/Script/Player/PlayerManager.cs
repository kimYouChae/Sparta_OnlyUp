using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("===Script===")]
    [SerializeField] private PlayerMovement movement;

    public PlayerMovement PlayerMovement { get => movement; }
}
