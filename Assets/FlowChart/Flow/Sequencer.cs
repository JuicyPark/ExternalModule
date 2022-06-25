using JuicyFlowChart;
using System.Collections;
using UnityEngine;

public class Sequencer : Flow
{
    public sealed override State Tick()
    {
        _state = State.Enable;

        State childState = State.Disable;
        foreach (Flow child in Children)
        {
            childState = child.Tick();
            if (childState == State.Disable)
            {
                child.ChangeToDisableState();
                return childState;
            }
        }
        return _state;
    }
}
