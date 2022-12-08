using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTarget : BTBaseNode
{
    List<GameObject> possibleTargets = new List<GameObject>();
    VariableGameObject currentTargetObject;
    int current = 0;
    int last = 0;

    public BTSetTarget(GameObject[] _waypoints, VariableGameObject _target)
    {
        foreach(GameObject point in _waypoints)
        {
            possibleTargets.Add(point);
        }
        currentTargetObject = _target;
    }

    public override TaskStatus Run()
    {
        if(currentTargetObject.Value == null)
        {
            currentTargetObject.Value = possibleTargets[Random.Range(0, possibleTargets.Count)];
        }

            if (!possibleTargets.Contains(currentTargetObject.Value))
            {
                currentTargetObject.Value = possibleTargets[last];
            }
        for(int i = 0; i < possibleTargets.Count; i ++)
        {

            if(possibleTargets[i] == currentTargetObject.Value)
            {
                current = i;
                last = i;
                continue;
            }
        }
        currentTargetObject.Value = possibleTargets[(current + 1) %possibleTargets.Count];
        return TaskStatus.Success;
    }
}
