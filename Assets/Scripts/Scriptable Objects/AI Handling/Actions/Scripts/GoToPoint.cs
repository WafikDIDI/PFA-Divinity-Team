using UnityEngine;

[CreateAssetMenu (menuName = "BluggableAI/Actions/GoToPoint")]
public class GoToPoint : AIAction {

    public override void Act (AIBaseStateController controller) {
        GoTo(controller);
    }

    private void GoTo (AIBaseStateController controller) {
        controller.Agent.SetDestination(controller.wayPointsList[controller.NextWayPoint].position);
        controller.Agent.isStopped = false;

        if(!controller.Agent.hasPath) {
            if (controller.Agent.pathPending) { return; }
            controller.NextWayPoint++;
            if(controller.NextWayPoint == controller.wayPointsList.Count) {
                controller.NextWayPoint = 0;
            }
            Debug.Log(controller.NextWayPoint);
        }
    }
}
