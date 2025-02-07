using UnityEngine;

public class RootNode : Node
{
    public Node child;
    public RootNode(Node node)
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
        RootNode rootNode = Instantiate(this);
        rootNode.child = child.Clone();
        return rootNode;
    }
}
