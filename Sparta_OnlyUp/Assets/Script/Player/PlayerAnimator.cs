using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public enum PlayerAnimation 
{
    Idle, 
    Walk,
    Jump
}

public class PlayerAnimator : MonoBehaviour
{
    [Header("===Animator===")]
    [SerializeField] private Animator animator;

    [Range(0, 1f)]
    public float distanceToGround;

    public Animator Animator { get => animator;  }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangePlayerState(PlayerAnimation state , bool flag) 
    {
        animator.SetBool(state.ToString() , flag);
    }

    public void AnimatorTrigger(PlayerAnimation state) 
    {
        animator.SetTrigger(state.ToString());
    }

    #region Foot Ik

    private void OnAnimatorIK(int layerIndex)
    {
        // 애니메이터가 실행되는 매 프레임 실행

        if(animator != null) 
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            // Debug.Log("IK Position: " + animator.GetIKPosition(AvatarIKGoal.LeftFoot));

            // left foot 
            // 플레이어 Root에서 Ankle_L 에 해당되는 부분 
            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

            // Debug.DrawRay(ray.origin, ray.direction * (distanceToGround + 1f), Color.red);

            if (Physics.Raycast(ray, out hit, distanceToGround + 1f , LayerManager.Instance.IgnorePlayerLayer)) 
            {
                //Debug.Log("레이케스트중");                   

                if(hit.transform.gameObject.layer == LayerManager.Instance.WalkableLayerInt) 
                {
                    //Debug.Log("Walkable인것만");

                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

            // Debug.DrawRay(ray.origin, ray.direction * (distanceToGround + 1f), Color.red);

            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, LayerManager.Instance.IgnorePlayerLayer))
            {
                //Debug.Log("레이케스트중");                   

                if (hit.transform.gameObject.layer == LayerManager.Instance.WalkableLayerInt)
                {
                    //Debug.Log("Walkable인것만");

                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }

    #endregion

}
