using UnityEngine;

namespace AI
{
    public class Wait : ActionNode
    {
        public float Duration = 1f;
        float _startTime;

        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            if (Time.time - _startTime > Duration)
            {
                return NodeState.Success;
            }

            return NodeState.Running;
        }
    }
}
