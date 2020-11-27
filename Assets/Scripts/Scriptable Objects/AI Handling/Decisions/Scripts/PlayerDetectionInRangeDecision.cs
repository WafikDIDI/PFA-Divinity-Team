using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Player Detection In Range")]
public class PlayerDetectionInRangeDecision : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        return PlayerInRange(controller);
    }

    private bool PlayerInRange (AIBaseStateController controller) {
        var playerPosition = AIBaseStateController.Player.transform.position;

        var distance = Vector3.Distance(playerPosition, controller.transform.position);

        if (distance < controller.DetectionRange) {
            return true;
        } else {
            return false;
        }
    }
}
