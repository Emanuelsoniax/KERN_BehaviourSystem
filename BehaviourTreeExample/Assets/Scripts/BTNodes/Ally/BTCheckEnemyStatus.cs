using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckEnemyStatus : BTBaseNode
{
    private Guard guard;
    private GameObject player;

    public BTCheckEnemyStatus(Guard _guard, GameObject _player)
    {
        guard = _guard;
        player = _player;
    }

    public override TaskStatus Run()
    {
        if(guard.Target.Value == player)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
