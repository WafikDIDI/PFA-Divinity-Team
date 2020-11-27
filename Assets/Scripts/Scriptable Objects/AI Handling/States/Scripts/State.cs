using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State", fileName ="New State")]
public class State : ScriptableObject {

    public string StateName = string.Empty;
    public AIAction[] actions = null;
    public Transition[] transitions = null;

    public void UpdateState(AIBaseStateController controller) {
        DoActions(controller);
        CheckTransition(controller);
    }

    private void DoActions (AIBaseStateController controller) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(controller);
        }
    }

    private void CheckTransition (AIBaseStateController controller) {
        for (int i = 0; i < transitions.Length; i++) {
            bool isDecisionSucceeded = transitions[i].decision.Decide(controller);
            if (isDecisionSucceeded) {
                controller.StateTransition(transitions[i].trueState);
            } else {
                controller.StateTransition(transitions[i].falseState);
            }
        }
    }

}
