using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Action : Flow
    {
        protected abstract void Start();
        protected abstract State Update();

        public sealed override State Tick()
        {
            if (_state == State.Disable)
            {
                _state = State.Enable;
                Start();
            }

            Update();

            if (Children.Count == 0)
                return _state;

            State childState = State.Disable;
            foreach (Flow child in Children)
            {
                childState = child.Tick();
            }
            return childState;
        }
    }
}