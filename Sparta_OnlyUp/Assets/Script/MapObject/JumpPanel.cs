using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPanel : MonoBehaviour
{
    [SerializeField]
    private float jumpPower;

    private void Start()
    {
        jumpPower = 15f;
    }

    // 밟으면 addforce하는 점프대

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager pl )) 
        {
            try { pl.PlayerMovement.Jump(jumpPower, Vector3.zero ); }
            catch (Exception e) { Debug.Log(e.ToString()); }
        }
    }
}
