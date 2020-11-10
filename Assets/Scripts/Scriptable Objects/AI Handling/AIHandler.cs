using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour {
    public static bool isAILookingForCover { get; private set; } = false;
    private static List<AIBaseStateController> aiControlles = new List<AIBaseStateController>();

    public static State LookingForCoverState = null;

    public bool isOneOfTheAIsLookingForCover () {
        foreach (AIBaseStateController ai in aiControlles) {
            if(ai.CurrentState == LookingForCoverState) {
                return true;
            }
        }
        return false;
    }

    public static void AddAI(AIBaseStateController controller) {
        aiControlles.Add(controller);
    }
}
