using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MoveVehicle : MonoBehaviour
{
    [SerializeField]
    private Transform startTrs;
    [SerializeField]
    private Transform endTrs;
    [SerializeField] private float moveSpeed; // 이동 속도 조절 변수
    [SerializeField] private float waitTime;  // 대기 시간 조절 변수

    [SerializeField] private bool isMove;

    private void Start()
    {
        moveSpeed = 0.3f;     // 이동 속도 조절 변수
        waitTime = 3f;      // 대기 시간 조절 변수
        isMove = false;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            isMove = true;

            // 시작점에서 끝점으로 이동
            yield return StartCoroutine(MoveFromTo(startTrs.position, endTrs.position));

            isMove = false;

            // 도착 후 대기
            yield return new WaitForSeconds(waitTime);

            isMove = true;

            // 끝점에서 시작점으로 이동
            yield return StartCoroutine(MoveFromTo(endTrs.position, startTrs.position));

            isMove = false;

            // 도착 후 대기
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator MoveFromTo(Vector3 start, Vector3 end)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        // 정확한 위치에 도달하도록 보장
        transform.position = end;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerManager.Instance.PlayerLayerInt) 
        {
            MainGameManager.Instance.player.transform.position = collision.contacts[0].point;
        }
    }
}
