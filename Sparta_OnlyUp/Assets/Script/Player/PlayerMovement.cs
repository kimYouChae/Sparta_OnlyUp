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
        // moveVec은 x와 y 밖에 없음
        // y가 양수면 앞쪽으로 이동
        // y가 음수면 뒤쪽으로 이동
        // x가 양수면 오른쪽으로 이동
        // x가 음수면 왼쪽으로 이동
        Vector3 dir = transform.forward * moveVector.y + transform.right * moveVector.x;
        dir *= speed;
        playerRb.velocity = new Vector3(dir.x, playerRb.velocity.y, dir.z);
        // 현재 물리엔진에 계산되고 있는 y 값을 넣어줘야함 
        //playerRb.velocity = dir;
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
        // 한번 눌리면 
        if(context.phase == InputActionPhase.Started) 
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector3.up * 5f , ForceMode.Impulse);
        }
    }

}
