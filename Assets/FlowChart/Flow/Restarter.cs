using JuicyFlowChart;
using System.Collections;
using UnityEngine;

public class Restarter : Flow
{
    private FlowChartRunner _runner;
    public sealed override State Tick()
    {
        if (_runner == null)
            _runner = GetComponent<FlowChartRunner>();

        _runner.Stop();
        return _state;
    }
}
