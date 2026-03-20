
namespace AI
{
    public class Sequence : CompositeNode
    {
        //Name to show in Behavior Tree Editor
        public string sequenceName = "New Sequence";
        int _index;

        protected override void OnStart()
        {
            _index = 0;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            var child = Children[_index];

            switch (child.Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Failure:
                    return NodeState.Failure;
                case NodeState.Success:
                    _index++;
                    break;
            }

            return _index == Children.Count ? 
                NodeState.Success : NodeState.Running;
        }
    }
}
