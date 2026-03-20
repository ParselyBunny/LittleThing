
public class DebugNode : ActionNode
{
    public string Message = "I'm outputting a debug message.";

    protected override void OnStart()
    {
        UnityEngine.Debug.Log("OK I started thinking.");
    }

    protected override NodeState OnUpdate()
    {
        UnityEngine.Debug.Log(Message);
        return NodeState.Success;
    }

    protected override void OnStop()
    {
        UnityEngine.Debug.Log("OK I'm done.");
    }
}
