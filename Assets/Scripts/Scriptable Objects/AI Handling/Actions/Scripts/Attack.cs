using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Actions/Attack")]
public class Attack : AIAction {

    public override void Act (AIBaseStateController controller) {
        Attacking(controller);
    }

    private void Attacking (AIBaseStateController controller) {
        controller.Agent.isStopped = true;

        var pointToLookAt =
            new Vector3(
                controller.Target.transform.position.x,
                controller.transform.position.y,
                controller.Target.transform.position.z
                );

        if (controller.Target) {
            controller.transform.LookAt(pointToLookAt);
        }
    }
}
