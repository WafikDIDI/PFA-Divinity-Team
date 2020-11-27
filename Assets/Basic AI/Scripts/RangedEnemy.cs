﻿using UnityEngine;

public class RangedEnemy : Enemy {

    // Attack Flee parameters
    [SerializeField] private float fleeRange = 10f;
    [SerializeField] private Color fleeRangeGizmoColor = Color.blue;

    // Attack cover parameters
    private bool isInCover = false;
    private Cover cover = null;

    protected override void OnDrawGizmos () {
        base.OnDrawGizmos();

        Gizmos.color = fleeRangeGizmoColor;
        Gizmos.DrawWireSphere(transform.position, fleeRange);
    }


    protected override void AttackState () {
        if (tragetDetected != null) {
            Vector3 lookdirection = new Vector3(
                    tragetDetected.position.x,
                    transform.position.y,
                    tragetDetected.position.z
                );

            transform.LookAt(lookdirection);
            tragetLastPosition = tragetDetected.position;
        }

        if (OverLap(fleeRange)) {
            Flee();
        } else if (OverLap(detectionRange)) {
            meshAgentComponent.isStopped = true;

            if (Cover.IsCoverValid(cover, tragetDetected) == false) {
                if (cover) { cover.IsEmpty = true; }
                cover = Cover.FindBestCover(tragetDetected, this, detectionRange);
                isInCover = false;
                if (cover) { cover.IsEmpty = false; }
            }

            if (isInCover) {

            } else if (cover != null) {
                meshAgentComponent.SetDestination(cover.transform.position);
                meshAgentComponent.isStopped = false;

                if(meshAgentComponent.hasPath == false) {
                    isInCover = true;
                }
            }
        } else if (OverLap(agroRange, out tragetDetected)) {
            GotoDetectedTarget();
        }


        if (tragetDetected == null) {
            currentState = AIState.Searching;
        }
    }

    private void Flee () {
        Vector3 directionToPlayer = transform.position - tragetDetected.position;
        Vector3 fleePosition = transform.position + directionToPlayer;
        meshAgentComponent.SetDestination(fleePosition);
        meshAgentComponent.isStopped = false;
    }

    private void GotoDetectedTarget () {
        meshAgentComponent.SetDestination(tragetDetected.position);
        meshAgentComponent.isStopped = false;
    }

}
