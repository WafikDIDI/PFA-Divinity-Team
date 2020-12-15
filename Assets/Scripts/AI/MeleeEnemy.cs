using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {

    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTimer = 0f;
    bool isAttacking = false;
    [SerializeField] private int attackDamage;

    protected override void OnDrawGizmos () {
        base.OnDrawGizmos();

        Gizmos.color = meleeHurtZoneGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, meleeHurtZone);
    }

    protected override void AttackState () {
        attackTimer += Time.deltaTime;

        Transform player = null;
        if (OverLap(meleeHurtZone, out player, "Player")) {
            meshAgentComponent.isStopped = true;

            if (attackCooldown <= attackTimer) {
                attackTimer -= attackCooldown;
                animationHandler.TriggerAttackAnimation(true);
                player.GetComponent<PlayerHealth>().healthSystem.Damage(attackDamage, 1);
            } else {
                animationHandler.TriggerIdleAnimation();
            }

        } else {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(tragetDetected.position);
            animationHandler.TriggerRunRangedAnimation();
            isAttacking = false;
            meshAgentComponent.speed = runningSpeed;
            tragetLastPosition = tragetDetected.position;
        }

        if (OverLap(agroRange) == false) {
            currentState = AIState.Searching;
            tragetDetected = null;
        }
    }

    protected override void OverWatch () {
        meshAgentComponent.isStopped = true;
        animationHandler.TriggerIdleAnimation();

        if (OverLap(detectionRange, out tragetDetected)) {
            currentState = AIState.Attack;
        }
    }

}
