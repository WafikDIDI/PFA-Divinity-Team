using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Decisions/CoverDecision")]
public class CoverDecision : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        return IsInCover(controller);
    }

    private bool IsInCover (AIBaseStateController controller) {
        return true;
    }
}
