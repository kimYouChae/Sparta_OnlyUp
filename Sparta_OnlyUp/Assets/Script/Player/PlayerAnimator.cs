using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
}
