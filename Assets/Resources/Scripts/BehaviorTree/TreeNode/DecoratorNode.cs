using UnityEngine;

public abstract class DecoratorNode : Node
{
    public Node child;
    public DecoratorNode(Node node)
    {
        this.child = node;
    }
    public override void SetTarget(GameObject target)
    {
        this.target = target;
        child.SetTarget(target);
    }
    public override Node.STATE Evaluate()
    {
        return child.Evaluate();
    }
    public override Node Clone()
    {
        DecoratorNode decoratorNode = Instantiate(this);
        decoratorNode.child = child.Clone();
        return decoratorNode;
    }
}
