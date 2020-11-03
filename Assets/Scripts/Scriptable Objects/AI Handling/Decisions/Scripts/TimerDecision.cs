using UnityEngine;

[CreateAssetMenu(menuName = "BluggableAI/Decisions/TimerDecision")]
public class TimerDecision : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        return isTimerPassed(controller);
    }

    private bool isTimerPassed (AIBaseStateController controller) {
        controller.Timer += Time.deltaTime;
        
        if(controller.Timer >= controller.SearchWaitingTime) {
            controller.Timer = 0f;
            return true;
        } else {
            return false;
        }
    }
}
