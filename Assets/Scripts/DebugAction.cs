using JuicyFlowChart;
using UnityEngine;

public class DebugAction : Action
{
    public string debugValue;
    protected override void Start()
    {
        Debug.Log(string.Format($"START : {debugValue}"));
    }

    protected override State Update()
    {
        Debug.Log(string.Format($"UPDATE : {debugValue}"));
        return State.Enable;
    }
}
