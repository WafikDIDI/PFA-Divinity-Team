using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol Action")]
public class PatrolAction : AIAction
{
    public override void Act (AIBaseStateController controller) {
        Patrol(controller);
    }

    private void Patrol (AIBaseStateController controller) {
        controller.Agent.SetDestination(controller.wayPointsList[controller.NextWayPoint].position);

        if(controller.Agent.hasPath == false) {
            if (controller.Agent.pathPending == false) {
                controller.NextWayPoint++;
                controller.NextWayPoint = controller.NextWayPoint % controller.wayPointsList.Count;
            }
        }
    }
}
