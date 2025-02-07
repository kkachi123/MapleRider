using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public List<Node> children = new List<Node>();
    public override void SetTarget(GameObject target)
    {
        this.target = target;
        foreach (Node child in children)
        {
            child.SetTarget(target);
        }
    }
    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.children = new List<Node>();
        foreach (Node child in children)
        {
            node.AddChild(child.Clone());
        }
        return node;
    }
    public virtual void AddChild(Node child)
    {
       children.Add(child);
    }
    public virtual void RemoveChild(Node child)
    {
       children.Remove(child);
    }

}
