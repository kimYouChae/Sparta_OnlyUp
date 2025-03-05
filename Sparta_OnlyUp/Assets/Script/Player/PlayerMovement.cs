using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("===Move===")]
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed = 3f;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove() 
    {
        Vector3 dir = transform.forward * moveVector.y + transform.right * moveVector.x;
        dir *= speed;
        playerRb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // 입력이 있으면 받아오기
        if (context.phase == InputActionPhase.Performed)
        {
            moveVector = context.ReadValue<Vector2>();
        }
        // 없으면 0 으로 
        else if(context.phase == InputActionPhase.Canceled) 
        {
            moveVector = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context) 
    { 
    
    }

}
