using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Back to Main")]
public class BackToMainDecision : AIDecision {
    public override bool Decide (AIBaseStateController controller) {
        return true;
    }
}
