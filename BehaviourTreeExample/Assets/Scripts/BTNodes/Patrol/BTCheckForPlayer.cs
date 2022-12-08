using UnityEditor;
using UnityEngine;

public class BTCheckForPlayer : BTBaseNode
{
    private Transform viewTransform;
    private VariableFloat sightRange;
    private VariableFloat viewingAngleInDegrees;
    private GameObject target;
    private int segments;

    public BTCheckForPlayer(Transform _viewTransform, VariableFloat _sightRange, VariableFloat _viewingAngleInDegrees, GameObject _target)
    {
        viewTransform = _viewTransform;
        sightRange = _sightRange;
        viewingAngleInDegrees = _viewingAngleInDegrees;
        target = _target;
        segments = ((int)viewingAngleInDegrees.Value/2);
    }

    public override TaskStatus Run()
    {
        if(RaycastSweep() == target)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }

    private GameObject RaycastSweep()
    {
        Vector3 startPos = viewTransform.position; //start position !
        //Vector3 targetPos = Vector3.zero; // variable for calculated end position

        float startAngle = -viewingAngleInDegrees.Value; // half the angle to the Left of the forward
        float finishAngle = viewingAngleInDegrees.Value; // half the angle to the Right of the forward

        // the gap between each ray (increment)
        var increments = (viewingAngleInDegrees.Value / segments);

        RaycastHit hit;
        GameObject gameObject = null;

        // step through and find each target point
        for (var i = startAngle; i < finishAngle; i += increments) // Angle from forward
        {
            Vector3 targetPos = (Quaternion.Euler(0, i, 0) * viewTransform.forward) * sightRange.Value + viewTransform.position;
            

            // linecast between points
            if (Physics.Raycast(startPos, targetPos, out hit))
            {
                //Debug.Log("Hit " + hit.collider.gameObject.name);
                gameObject = hit.collider.gameObject;
                if(gameObject == target)
                {
                    return target;
                }
            }
            // to show ray just for testing
            Debug.DrawLine(startPos, targetPos, Color.green);
        }
        return gameObject;
    }

}
