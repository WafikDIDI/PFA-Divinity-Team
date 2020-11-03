using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Actions/Searching")]
public class Searching : AIAction {
    public override void Act (AIBaseStateController controller) {
        Search(controller);
    }

    private void Search (AIBaseStateController controller) {
        if (controller.TargetLastSeenPosition != null) {
            controller.Agent.SetDestination((Vector3)controller.TargetLastSeenPosition);
            controller.Agent.isStopped = false;
        }
    }
}
