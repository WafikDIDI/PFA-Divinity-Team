using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack Action")]
public class AttackAction : AIAction
{
    public override void Act (AIBaseStateController controller) {
        CheckCoverState(controller);
    }

    private void CheckCoverState (AIBaseStateController controller) {
        if (!controller.Agent.hasPath) {
            if (controller.Cover.isValid) {
                controller.isInCover = true;
            }
        }
    }
}
