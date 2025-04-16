using System.Collections.Generic;
using UnityEngine;
public class PararellNode : CompositeNode
{
    bool allSuccess;
    bool running;
    public PararellNode()
    { 
        children = new List<Node>();
        allSuccess = false;
        running = false;
    }

    public override Node.STATE Evaluate()
    {
        allSuccess = true;
        running = false;
        for (int i = 0; i < children.Count; i++)
        {
            switch (children[i].Evaluate())
            {
                case Node.STATE.RUN:
                    running = true;
                    continue;
                case Node.STATE.FAILED:
                    allSuccess = false;
                    continue;
                case Node.STATE.SUCCESS:
                    continue;
            }
        }
        if(allSuccess)
            return Node.STATE.SUCCESS;
        if (running)
            return Node.STATE.RUN;
        return Node.STATE.FAILED;
    }
}
