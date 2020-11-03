using System;

[Serializable]
public class Transition {
    public AIDecision decision = null;
    public State trueState = null, falseState = null;
}
