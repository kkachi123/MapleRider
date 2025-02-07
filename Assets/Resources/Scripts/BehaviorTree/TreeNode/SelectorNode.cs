using System.Collections.Generic;
using UnityEngine;
public class SelectorNode : CompositeNode
{
    public SelectorNode()
    {
        children = new List<Node>();
    }
    public override Node.STATE Evaluate()
    {
        for(int i = 0; i < children.Count; i++)
        {
            switch(children[i].Evaluate())
            {
                case Node.STATE.RUN:
                    return Node.STATE.RUN;
                case Node.STATE.SUCCESS:
                    return Node.STATE.SUCCESS;
                case Node.STATE.FAILED:
                    continue;
            }
        }
        return Node.STATE.FAILED;
    }
}
