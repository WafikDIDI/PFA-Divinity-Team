using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Decisions/TragetLost")]
public class AttackDecision : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        bool isLost = isTargetLost(controller);
        return isLost;
    }

    private bool isTargetLost (AIBaseStateController controller) {
        if (controller.Target == null) {
            return true;
        } else {
            return false;
        }
    }
}
