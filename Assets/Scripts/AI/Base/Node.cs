using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Base class for behavior tree nodes.
/// </summary>
public abstract class Node : ScriptableObject
{
    public enum NodeState
    {
        Running,
        Failure,
        Success,
    }
    [HideInInspector]
    public string Guid;
    [HideInInspector]
    public Vector2 Position;
    private bool _started = false;

    //Reference to owner gameobj
    public GameObject self;

    [HideInInspector]
    public NodeState State { get; private set; }

    public NodeState Update()
    {
        if (!_started)
        {
            OnStart();
            _started = true;
        }

        State = OnUpdate();

        if (State == NodeState.Failure || State == NodeState.Success)
        {
            OnStop();
            _started = false;
        }

        return State;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    protected abstract void OnStart();
    protected abstract NodeState OnUpdate();
    protected abstract void OnStop();
}
