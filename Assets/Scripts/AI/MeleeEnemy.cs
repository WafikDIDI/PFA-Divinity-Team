using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {

    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;

    [SerializeField] private float attackCooldown = 10f;
    [SerializeField] private float attackTimer = 0f;
    [SerializeField] bool isAttacking = false;

    protected override void OnDrawGizmos () {
        base.OnDrawGizmos();

        Gizmos.color = meleeHurtZoneGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, meleeHurtZone);
    }

    protected override void Awake () {
        base.Awake();

        attackTimer = attackCooldown;
    }

    protected override void AttackState () {
        attackTimer += Time.deltaTime;

        if (OverLap(meleeHurtZone)) {
            meshAgentComponent.isStopped = true;

            if (attackCooldown <= attackTimer) {
                attackTimer -= attackCooldown;
                animationHandler.TriggerAttackAnimation(true);
                print("1");
            } else {
                animationHandler.TriggerIdleAnimation();
                print("2");
            }
        } else {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(tragetDetected.position);
            animationHandler.TriggerRunAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = tragetDetected.position;
        }

        if (OverLap(agroRange) == false) {
            currentState = AIState.Searching;
            tragetDetected = null;
        }
    }

}
