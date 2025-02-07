using System.Collections.Generic;
using UnityEngine;
public class SequenceNode : CompositeNode
{
    public SequenceNode()
    {
        children = new List<Node>();
    }
    public override Node.STATE Evaluate()
    {
        for (int i = 0; i < children.Count; i++)
        {
            switch (children[i].Evaluate())
            {
                case Node.STATE.RUN:
                    return Node.STATE.RUN;
                case Node.STATE.FAILED:
                    return Node.STATE.FAILED;
                case Node.STATE.SUCCESS:
                    continue;
            }
        }
        return Node.STATE.SUCCESS;
    }
}
