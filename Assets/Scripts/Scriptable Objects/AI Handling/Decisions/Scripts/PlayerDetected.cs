using UnityEngine;

[CreateAssetMenu (menuName ="BluggableAI/Decisions/PlayerDetected")]
public class PlayerDetected : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        bool playerDetected = Look(controller);
        return playerDetected;
    }

    private bool Look (AIBaseStateController controller) {
        return controller.IsPlayerDetected;
    }
}
