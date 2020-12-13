﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AIAnimationHandler : MonoBehaviour
{
    private Animator animatorReference = null;

    private void Awake () {
        animatorReference = GetComponent<Animator>();
    }

    public void TriggerWalkAnimation () {
        animatorReference.SetBool("Walk", true);
        animatorReference.SetBool("Run", false);
        animatorReference.SetBool("Attack", false);
    }

    public void TriggerRunAnimation () {
        animatorReference.SetBool("Walk", true);
        animatorReference.SetBool("Run", true);
        animatorReference.SetBool("Attack", false);
    }

    public void TriggerIdleAnimation () {
        animatorReference.SetBool("Run", false);
        animatorReference.SetBool("Walk", false);
    }

    public void TriggerSearchingAnimation () {
        animatorReference.SetBool("Walk", false);
        animatorReference.SetBool("Run", false);
        animatorReference.SetBool("Searching", true);
    }

    public void TriggerAttackAnimation (bool state) {
        
        animatorReference.SetBool("Attack", state);

    }
}
