using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Decisions/HasPathDecision")]
public class HasPathDecision : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        return OnSeachEnd(controller);
    }

    private bool OnSeachEnd (AIBaseStateController controller) {
        if (!controller.Agent.hasPath) {
            return true;
        } else {
            return false;
        }
    }

}
