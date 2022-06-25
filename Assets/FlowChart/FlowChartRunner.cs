using UnityEngine;

namespace JuicyFlowChart
{
    public class FlowChartRunner : MonoBehaviour
    {
        [SerializeField]
        private FlowChart _flowChart;
        private Flow _root;

        public FlowChart FlowChart { get => _flowChart; }
        public Flow Root { get => _root; }

        private void Start()
        {
            if(_flowChart == null)
            {
                Debug.LogWarning("Not Found FlowChart");
                return;
            }

            _root = _flowChart.Clone(gameObject);
        }

        private void Update()
        {
            if (_root == null)
                return;

            _root.Tick();
        }

        public void Stop()
        {
            _root.ChangeToDisableState();
        }
    }
}