using System;
using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{    public abstract class Flow
    {
        public enum State
        {
            Enable,
            Disable
        }

        protected State _state = State.Disable;
        protected GameObject gameObject;
        protected Transform transform;

        private List<Flow> _children = new List<Flow>();
        private int _nodeID;

        public State CurrentState { get => _state; }
        public List<Flow> Children { get => _children; set => _children = value; }
        public int NodeID { get => _nodeID; set => _nodeID = value; }

        public abstract State Tick();

        internal void ChangeToDisableState()
        {
            if (_state == State.Disable)
                return;

            _state = State.Disable;
            foreach (Flow child in _children)
            {
                child.ChangeToDisableState();
            }
        }

        protected T GetComponent<T>() where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>();
        }

        public void SetGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }
    }
}