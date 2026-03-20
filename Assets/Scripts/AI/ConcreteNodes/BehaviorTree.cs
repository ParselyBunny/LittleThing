
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class BehaviorTree : ScriptableObject
{
    public Node Root;
    public Node.NodeState State = Node.NodeState.Running;
    public List<Node> nodes = new();

    public Node.NodeState Update()
    {
        if (State == Node.NodeState.Running)
        {
            State = Root.Update();
        }

        return State;
    }

    public Node CreateNode(Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.Guid = GUID.Generate().ToString();
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();

    }

    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.Child = child;
        }

        RootNode root = parent as RootNode;
        if (root)
        {
            root.Child = child;
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            composite.Children.Add(child);
        }
    }

    public void RemoveChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.Child = null;
        }

        RootNode root = parent as RootNode;
        if (root)
        {
            root.Child = null;
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            composite.Children.Remove(child);
        }
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> list = new();

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.Child != null)
        {
            list.Add(decorator.Child);
        }

        RootNode root = parent as RootNode;
        if (root && root.Child != null)
        {
            list.Add(root.Child);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            list = composite.Children;
        }

        return list;
    }

    public BehaviorTree Clone()
    {
        BehaviorTree tree = Instantiate(this);
        tree.Root = tree.Root.Clone();
        return tree;
    }

    //Set owner gameobj as self for all nodes in behavior tree
    public void SetSelf(GameObject self)
    {
        foreach(Node node in nodes)
        {
            node.self = self;            
        }
    }
}
