using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Take Cover")]
public class TakeCoverDecision : AIDecision
{
    public override bool Decide (AIBaseStateController controller) {
        return IsNeedingCover(controller);
    }

    private bool IsNeedingCover (AIBaseStateController controller) {
        if(controller.Cover == null) {
            AIHandler.isAILookingForCover = true;
            controller.Cover = Cover.NearestValidCover(controller.transform);
            controller.isInCover = false;
            return false;
        } else {

            if(controller.Cover.isValid == false) {
                controller.Cover = Cover.NearestValidCover(controller.transform);
                controller.isInCover = false;
            }

            var distance = Vector3.Distance(
                AIBaseStateController.Player.transform.position, 
                controller.Cover.transform.position
                );

            if (distance < controller.ShootingRange && !controller.isInCover) {
                return true;
            } else {
                return false;
            }

        }
    }
}
