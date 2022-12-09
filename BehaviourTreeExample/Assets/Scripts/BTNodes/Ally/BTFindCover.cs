using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFindCover : BTBaseNode
{
    private Transform transform;
    private GameObject[] placesToHide;
    VariableGameObject target;

    public BTFindCover(Transform _transform, VariableGameObject _target, GameObject[] _placesToHide)
    {
        transform = _transform;
        target = _target;
        placesToHide = _placesToHide;
    }

    public override TaskStatus Run()
    {
        GameObject closestHidingPlace = null;
        float distanceToHidingPlace = float.PositiveInfinity;

        foreach (GameObject hidingPlace in placesToHide)
        {
            if(Vector3.Distance(transform.position, hidingPlace.transform.position) < distanceToHidingPlace)
            {
                closestHidingPlace = hidingPlace;
            }
        }

        if(closestHidingPlace == null)
        {
            return TaskStatus.Failed;
        }

        target.Value = closestHidingPlace;
        //Vector3 direction = (closestHidingPlace.transform.position - guard.transform.position).normalized;
        //target.Value.transform.position = closestHidingPlace.transform.position + (2 * direction);

        return TaskStatus.Success;
    }
}
