using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Condition : Flow
    {
        protected abstract bool Check();

        public sealed override State Tick()
        {
            if (Check())
            {
                _state = State.Enable;
                State childState = State.Disable;
                foreach (Flow child in Children)
                {
                    childState = child.Tick();
                }
                return childState;
            }
            else
            {
                if (_state == State.Enable)
                {
                    _state = State.Disable;
                    foreach (Flow child in Children)
                    {
                        child.ChangeToDisableState();
                    }
                }
            }

            return _state;
        }
    }
}