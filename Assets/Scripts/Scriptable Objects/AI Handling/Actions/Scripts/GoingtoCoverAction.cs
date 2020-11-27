using System;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Going to Cover")]
public class GoingtoCoverAction : AIAction {
    public override void Act (AIBaseStateController controller) {
        GotoCover(controller);
    }

    private void GotoCover (AIBaseStateController controller) {
        if(controller.Agent.stoppingDistance > controller.Agent.remainingDistance) {
            controller.isInCover = true;
        } else {
            controller.Agent.SetDestination(controller.Cover.transform.position);
            controller.isInCover = false;
        }
    }
}
