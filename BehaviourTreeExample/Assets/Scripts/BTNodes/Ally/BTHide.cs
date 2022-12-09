using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTHide : BTBaseNode
{
    private NavMeshAgent agent;
    private Guard guard;
    private VariableGameObject target;
    private VariableFloat movementSpeed;

    public BTHide(Guard _guard, VariableGameObject _target, VariableFloat _movementSpeed, NavMeshAgent _agent)
    {
        guard = _guard;
        target = _target;
        movementSpeed = _movementSpeed;
        agent = _agent;
    }

    public override TaskStatus Run()
    {
        Vector3 direction = (target.Value.transform.position - guard.transform.position).normalized;
        //target.Value.transform.position = target.Value.transform.position + (2 * direction);

        agent.SetDestination(target.Value.transform.position + (2 * direction));
        agent.speed = movementSpeed.Value;

        GameObject targetPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        targetPoint.transform.position = target.Value.transform.position + (2 * direction);
        targetPoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        agent.SetDestination(targetPoint.transform.position);
        GameObject.Destroy(targetPoint,0.1f);

        if((Vector3.Distance(agent.transform.position, target.Value.transform.position) <= 0.5f)){
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
