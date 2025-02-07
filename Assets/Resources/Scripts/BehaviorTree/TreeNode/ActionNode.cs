using System;
using UnityEngine;
public class ActionNode : Node
{
    public Func<Node.STATE> action;
    [SerializeField] private string methodName;
    Component methodComponent;
    public ActionNode(Func<Node.STATE> action)
    {
        this.action = action;
    }
    public override void SetTarget(GameObject target)
    {
        this.target = target;
        System.Type componentType = System.Type.GetType("BossBehavior");
        methodComponent = target.GetComponent(componentType);
        if (methodComponent == null)
            action = null;
        else
            action = (Func<Node.STATE>)Delegate.CreateDelegate(typeof(Func<Node.STATE>), methodComponent, methodName);
    }
    public override Node.STATE Evaluate()
    {
        if (action == null)
        {
            return Node.STATE.FAILED;
        }
        return action.Invoke();
    }

}
