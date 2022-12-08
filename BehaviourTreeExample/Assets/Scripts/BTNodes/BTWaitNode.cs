using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWaitNode : BTBaseNode
{
    private float waitTime;
    private float currentTime;
    public BTWaitNode(float _waitTime)
    {
        waitTime = _waitTime;
    }

    public override TaskStatus Run()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            currentTime = 0;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;

    }
}
