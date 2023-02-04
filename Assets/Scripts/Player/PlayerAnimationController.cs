using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayJumpingAnim()
    {
        animator.SetInteger("state", 2);
    }
    public void PlayFallingAnim()
    {
        animator.SetInteger("state", 3);
    }
    public void PlayIdleAnim()
    {
        animator.SetInteger("state", 0);
    }

    public void PlayRunningAnim()
    {
        animator.SetInteger("state", 1);
    }
    public void TiggerAttackAnim()
    {
        animator.SetTrigger("Attacking");
    }
}
