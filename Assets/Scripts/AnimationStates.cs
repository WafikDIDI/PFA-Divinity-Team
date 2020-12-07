using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    public Animator animator;

    public void ChangeAnimationState(float speed)
    {
        animator.SetFloat("Speed", speed);     
    }

    public void AimAnimation(bool verif) => animator.SetBool("aiming", verif);

    public void ShootingAnimation(bool verif) => animator.SetBool("shooting", verif);
}
