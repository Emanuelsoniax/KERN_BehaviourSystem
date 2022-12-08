using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequenceNode : BTBaseNode
{
    private BTBaseNode[] children;
    private int currentIndex = 0;

    public BTSequenceNode(params BTBaseNode[] _children)
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
                    currentIndex = 0;
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

public class BTDebug : BTBaseNode
{
    private string debugMessage;
    public BTDebug(string _debugMessage)
    {
        debugMessage = _debugMessage;
    }

    public override TaskStatus Run()
    {
        Debug.Log(debugMessage);
        return TaskStatus.Success;
    }
}