using UnityEngine;

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
        }

        if (OverLap(agroRange) == false) {
            currentState = AIState.Searching;
            tragetDetected = null;
        }
    }

    protected override void OverWatch () {
        
    }

    protected override void Patrol () {
        meshAgentComponent.isStopped = false;
        meshAgentComponent.SetDestination(wayPoints[currentWayPointIndex].position);

        if (meshAgentComponent.hasPath == false) {
            if(meshAgentComponent.pathPending == false) {
                currentWayPointIndex ++;
                currentWayPointIndex %= wayPoints.Count;
            }
        }

        if (CheckIfPlayerInRange()) {
            currentState = AIState.Attack;
        }
    }

    protected override void Searching () {
        
    }

}
