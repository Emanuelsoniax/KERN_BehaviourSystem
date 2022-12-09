using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTFollowPlayer : BTBaseNode
{
    private NavMeshAgent agent;
    private GameObject player;

   public BTFollowPlayer(GameObject _player, NavMeshAgent _agent)
    {
        player = _player;
        agent = _agent;
    }

    public override TaskStatus Run()
    {

        agent.SetDestination(player.transform.position);
        if (Vector3.Distance(agent.transform.position, player.transform.position) <= 5)
        {
            agent.SetDestination(agent.transform.position);
            return TaskStatus.Success;
        }
        else return TaskStatus.Running;
    }
}
