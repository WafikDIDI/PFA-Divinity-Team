using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {

    [Space]
    [SerializeField] private float meleeHurtZone = 2f;
    [SerializeField] private Color meleeHurtZoneGizmoColor = Color.red;

    protected override void OnDrawGizmos () {
        base.OnDrawGizmos();

        Gizmos.color = meleeHurtZoneGizmoColor;
        Gizmos.DrawWireSphere(this.transform.position, meleeHurtZone);
    }

    protected override void AttackState () {
        if (OverLap(meleeHurtZone)) {
            meshAgentComponent.isStopped = true;
            print("Attack");
        } else {
            meshAgentComponent.isStopped = false;
            meshAgentComponent.SetDestination(tragetDetected.position);
            tragetLastPosition = tragetDetected.position;
        }

        if (OverLap(agroRange) == false) {
            currentState = AIState.Searching;
            tragetDetected = null;
        }
    }

}
