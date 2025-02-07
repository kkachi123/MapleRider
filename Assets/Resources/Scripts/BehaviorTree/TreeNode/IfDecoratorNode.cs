using System;
using UnityEngine;
public class IfDecoratorNode : DecoratorNode
{
    [SerializeField] private string methodName;
    Component methodComponent;
    private System.Func<bool> condition;

    public IfDecoratorNode(Node child, System.Func<bool> condition) : base(child)
    {
        this.condition = condition;
    }
    public override void SetTarget(GameObject target)
    {
        this.target = target;
        System.Type componentType = System.Type.GetType("BossBehavior");
        methodComponent = target.GetComponent(componentType);
        if (methodComponent == null)
            condition = null;
        else
            condition = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), methodComponent, methodName);
        child.SetTarget(target);
    }

    public override Node.STATE Evaluate()
    {
        if(child == null)
        {
            return Node.STATE.FAILED;
        }
        // 조건이 참일 때만 자식 노드를 평가
        if (condition.Invoke())
        {
            return base.Evaluate();
        }
        return Node.STATE.FAILED;
    }

    public override Node Clone()
    {
        IfDecoratorNode ifDecoratorNode = Instantiate(this);
        ifDecoratorNode.child = child.Clone();
        return ifDecoratorNode;
    }
}
