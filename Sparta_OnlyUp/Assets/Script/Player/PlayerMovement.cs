using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("===Object===")]
    [SerializeField] private Transform cameraTrs;

    [Header("===Move===")]
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float speed = 0.9f;

    [Header("===Jump===")]
    [SerializeField] private float jumpPower = 7f;

    [Header("===Rotate===")]
    [SerializeField] private Vector2 mouseDelta;        // 마우스 움직임 델타
    [SerializeField] private float currentY;   // 현재 회전 상태 Y
    [SerializeField] private float rotationBoundary = 80f;
    [SerializeField] private float sensitivity;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        sensitivity = 3f;

        ChangeCursorState(CursorLockMode.Locked);
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void LateUpdate()
    {
        RotateCamera();
    }

    #region 플레이어 움직임 
    
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
        // 입력 시작 시 walk
        if(context.phase == InputActionPhase.Started) 
        {
            PlayerManager.Instance.PlayerAnimator.ChangePlayerState(PlayerAnimation.Walk, true);
        }
        // 입력이 있으면 받아오기
        if (context.phase == InputActionPhase.Performed)
        {
            moveVector = context.ReadValue<Vector2>();
        }
        // 없으면 0 으로 
        else if(context.phase == InputActionPhase.Canceled) 
        {
            moveVector = Vector2.zero;

            // 애니메이션 종료
            PlayerManager.Instance.PlayerAnimator.ChangePlayerState(PlayerAnimation.Walk, false);
        }
    }
    #endregion

    #region 플레이어 점프

    public void OnJump(InputAction.CallbackContext context) 
    { 
        // 한번 눌리면 
        if(context.phase == InputActionPhase.Started) 
        {
            Jump(jumpPower);

            // 애니메이션 실행
            PlayerManager.Instance.PlayerAnimator.AnimatorTrigger(PlayerAnimation.Jump);
        }
    }

    public void Jump(float jumpPower) 
    {
        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    #endregion

    #region 플레이어/카메라 회전 

    private void RotateCamera()
    {
        // 플레이어는 rotation의 y값만 바껴야한다
        // 카메라는 rotation의 x값만 바껴야 한다

        currentY += mouseDelta.y * sensitivity;

        float newY = Mathf.Clamp(currentY, -rotationBoundary, rotationBoundary) ;

        // 카메라 회전 
        cameraTrs.localEulerAngles = new Vector3( -newY, 0, 0);

        // 플레이어 회전 
        transform.eulerAngles += new Vector3(0, mouseDelta.x * sensitivity, 0);
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        // Delta값 :
        // 화면의 중앙을 (0, 0) 기준으로
        // 마우스를 빠르게 움직일수록 절대값이 커짐
        //      마우스를 오른쪽으로 빠르게 이동: (15, 0)
        //      마우스를 왼쪽으로 천천히 이동: (-2, 0)
        mouseDelta = context.ReadValue<Vector2>();
        // Debug.Log($"Mouse Delta: {mouseDelta}");
    }

    private void ChangeCursorState(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }

    #endregion

    #region
    public void OnInteract(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started) 
        {
            PlayerManager.Instance.PlayerInteraction.PlayerInputInteratKey();
        }

    }

    #endregion
}
