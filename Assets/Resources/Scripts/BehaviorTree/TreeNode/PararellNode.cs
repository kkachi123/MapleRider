using System.Collections.Generic;
using UnityEngine;
public class PararellNode : CompositeNode
{
    bool allSuccess;
    bool Running;
    public PararellNode()
    { 
        children = new List<Node>();
        allSuccess = false;
        Running = false;
    }

    public override Node.STATE Evaluate()
    {
        allSuccess = false;
        Running = false;
        for (int i = 0; i < children.Count; i++)
        {
            switch (children[i].Evaluate())
            {
                case Node.STATE.RUN:
                    Running = true;
                    continue;
                case Node.STATE.FAILED:
                    continue;
                case Node.STATE.SUCCESS:
                    allSuccess = true;
                    continue;
            }
        }
        if(allSuccess)
        {
            return Node.STATE.SUCCESS;
        }
        if (Running)
        {
            return Node.STATE.RUN;
        }
        return Node.STATE.FAILED;
    }
}
