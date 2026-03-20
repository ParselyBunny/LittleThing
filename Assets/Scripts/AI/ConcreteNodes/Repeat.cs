
namespace AI
{
    public class Repeat : DecoratorNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            Child.Update();
            return NodeState.Running;
        }
    }
}
