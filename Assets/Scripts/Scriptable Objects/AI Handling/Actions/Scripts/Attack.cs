using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Actions/Attack")]
public class Attack : AIAction {
    public override void Act (AIBaseStateController controller) {
        Attacking(controller);
    }

    private void Attacking (AIBaseStateController controller) {
        controller.Agent.isStopped = true;

        if (controller.Target) {
            controller.transform.LookAt(controller.Target.transform);
        }
    }
}
