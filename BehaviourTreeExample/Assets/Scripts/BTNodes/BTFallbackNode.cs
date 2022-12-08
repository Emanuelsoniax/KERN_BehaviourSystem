using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFallbackNode : BTBaseNode
{
    private BTBaseNode[] children;
    private int currentIndex = 0;

    public BTFallbackNode(BTBaseNode[] _children)
    {
        children = _children;
    }
    public override TaskStatus Run()
    {
        for (; currentIndex < children.Length; currentIndex++)
        {
            TaskStatus status = children[currentIndex].Run();
            switch (status)
            {
                case TaskStatus.Failed:
                    currentIndex++;
                    return TaskStatus.Failed;
                case TaskStatus.Running:
                    return TaskStatus.Running;
                case TaskStatus.Success: break;
            }
        }
        currentIndex = 0;
        return TaskStatus.Success;
    }
}
