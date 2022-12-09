using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToTarget : BTBaseNode
{
    private VariableFloat movementSpeed;
    private VariableFloat stopDistance;
    private NavMeshAgent agent;
    private VariableGameObject target;

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
        if(Vector3.Distance(agent.transform.position, target.Value.transform.position) <= 0.5f)
        {
            agent.SetDestination(agent.transform.position);
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }

}
