using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator animator;

    public void SetRunAnimation()
    {
        animator.SetTrigger("Run");
    }

    public void ResetRunAnimation()
    {
        animator.ResetTrigger("Run");
    }

    public void SetJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    public void ResetJumpAnimation()
    {
        animator.ResetTrigger("Jump");
    }

    public void SetShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void ResetShootAnimation()
    {
        animator.ResetTrigger("Shoot");
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;    
    }
}
