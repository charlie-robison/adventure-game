using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator animator;

    public void setRunAnimation()
    {
        animator.SetTrigger("Run");
    }

    public void resetRunAnimation()
    {
        animator.ResetTrigger("Run");
    }

    public void setJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    public void resetJumpAnimation()
    {
        animator.ResetTrigger("Jump");
    }

    public void setShootAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void resetShootAnimation()
    {
        animator.ResetTrigger("Shoot");
    }

    public void setAnimationSpeed(float speed)
    {
        animator.speed = speed;    
    }
}
