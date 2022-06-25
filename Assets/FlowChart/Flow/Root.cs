namespace JuicyFlowChart
{
    public class Root : Flow
    {
        public sealed override State Tick()
        {
            _state = State.Enable;
            foreach (Flow child in Children)
            {
                child.Tick();
            }
            return _state;
        }
    }
}