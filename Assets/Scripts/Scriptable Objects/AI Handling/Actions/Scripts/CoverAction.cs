using System;
using UnityEngine;


[CreateAssetMenu(menuName = "BluggableAI/Actions/Going to Cover")]
public class CoverAction : AIAction {
    public override void Act (AIBaseStateController controller) {
        GoToCover(controller);
    }

    private void GoToCover (AIBaseStateController controller) {
        CheckOrGetValidCover(controller);

        controller.Agent.SetDestination(controller.Cover.transform.position);
    }

    private void CheckOrGetValidCover (AIBaseStateController controller) {
        if (controller.Cover != null) {
            if(controller.Cover.isValid == false) {
                controller.Cover = Cover.NearestValidCover(controller.transform);
            }
        } else {
            controller.Cover = Cover.NearestValidCover(controller.transform);
        }
    }
}
