using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpLauncher : MonoBehaviour
{
    [SerializeField] private Transform loancherTrs;
    [SerializeField] private float jumpLauncherPower;

    private void Start() 
    {
        jumpLauncherPower = 25f;

        StartCoroutine(jumperCoroutine());
    }

    IEnumerator jumperCoroutine()
    {
        while (true) 
        {
            RaycastHit hit;
            Ray ray = new Ray(loancherTrs.position , loancherTrs.right);

            Debug.DrawRay(ray.origin , ray.direction * 15f , Color.red  );

            // 플레이어만 검사 
            if (Physics.Raycast(ray, out hit, 15f, LayerManager.Instance.PlayerLayer)) 
            {
                break;            
            }

            // 매프레임 검사 
            yield return null;
        }

        // 플레이어가 검출되었으면 
        // 5초 카운터 후
        for (int i = 5; i >= 1; i--) 
        {
            // UI에 카운트 표시 
            MainGameManager.Instance.mainGameUi.PrintCount(i);

            yield return new WaitForSeconds(1f);
        }
        MainGameManager.Instance.mainGameUi.PrintCount(0);

        // 플레이어 위치 따라 addforce방향 다르게 
        // x가 27 이하면 왼
        // 27 ~ 31 이면 중간
        // 31 이상이면 오른 
        if (MainGameManager.Instance.player.transform.position.x < 27)
        {
            // 왼
            PlayerManager.Instance.PlayerMovement.Jump(jumpLauncherPower , -transform.right );
        }
        else if (MainGameManager.Instance.player.transform.position.x <= 31)
        {
            // 중
            PlayerManager.Instance.PlayerMovement.Jump(jumpLauncherPower, transform.forward);
        }
        else
        {
            // 오 
            PlayerManager.Instance.PlayerMovement.Jump(jumpLauncherPower, transform.right);
        }
    }
}
