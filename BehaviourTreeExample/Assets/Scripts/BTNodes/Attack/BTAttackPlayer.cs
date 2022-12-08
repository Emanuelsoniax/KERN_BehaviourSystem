using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAttackPlayer : BTBaseNode
{
    private Player player;
    private float damage;
    private GameObject parent;

    public BTAttackPlayer(Player _player, float _damage, GameObject _parent)
    {
        player = _player;
        damage = _damage;
        parent = _parent;
    }

    public override TaskStatus Run()
    {
        player.TakeDamage( parent, (int)damage);
        return TaskStatus.Success;
    }
}
