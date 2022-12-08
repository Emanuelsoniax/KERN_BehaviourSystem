using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToTarget : BTBaseNode
{
    VariableFloat movementSpeed;
    VariableFloat stopDistance;
    NavMeshAgent agent;
    VariableGameObject target;

    public BTMoveToTarget(VariableGameObject _target, VariableFloat _movementSpeed, VariableFloat _stopDistance, NavMeshAgent _agent)
    {
        target = _target;
        movementSpeed = _movementSpeed;
        stopDistance = _stopDistance;
        agent = _agent;
    }

    public override TaskStatus Run()
    {
        agent.SetDestination(target.Value.transform.position);
        agent.speed = movementSpeed.Value;
        if(Vector3.Distance(agent.transform.position, target.Value.transform.position) <= stopDistance.Value)
        {
            Debug.Log("joeee");
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }

}
