using JuicyFlowChart;
using System.Collections;
using UnityEngine;

public class RandomSelector : Flow
{
    private int _randomIndex;
    public sealed override State Tick()
    {
        if (_state == State.Disable)
        {
            _state = State.Enable;
            _randomIndex = Random.Range(0, Children.Count);
        }

        if (Children.Count == 0)
            return _state;

        return Children[_randomIndex].Tick();
    }
}