using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParallelNode : BTBaseNode
{
    private BTBaseNode[] children;

    public BTParallelNode(BTBaseNode[] _children)
    {
        children = _children;
    }

    public override TaskStatus Run()
    {
        int failedChildren = 0;
        foreach(BTBaseNode child in children)
        {
            if(child.Run() == TaskStatus.Failed)
            {
                failedChildren++;
            }
        }

        if(failedChildren > 0)
        {
            return TaskStatus.Failed;
        }

        return TaskStatus.Running;
    }
}
