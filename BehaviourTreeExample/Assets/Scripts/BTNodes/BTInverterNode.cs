using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverterNode : BTBaseNode
{
    private BTBaseNode childNode;

    public BTInverterNode(BTBaseNode _childNode)
    {
        childNode = _childNode;
    }

    public override TaskStatus Run()
    {
        if(childNode.Run() == TaskStatus.Failed)
        {
            return TaskStatus.Success;
        }

        if(childNode.Run() == TaskStatus.Success)
        {
            return TaskStatus.Failed;
        }
        
        return childNode.Run();
    }
}
