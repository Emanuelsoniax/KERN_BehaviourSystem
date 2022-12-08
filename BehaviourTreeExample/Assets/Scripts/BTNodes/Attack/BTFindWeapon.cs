using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTFindWeapon : BTBaseNode
{
    private GameObject[] weapons;
    private VariableGameObject target;
    private Transform transform;

    public BTFindWeapon(GameObject[] _weapons, VariableGameObject _target, Transform _position)
    {
        weapons = _weapons;
        target = _target;
        transform = _position;
    }

    public override TaskStatus Run()
    {
        if(FindWeapon() == null)
        {
            return TaskStatus.Failed;
        }
        target.Value = FindWeapon();
        return TaskStatus.Success;
    }

    private GameObject FindWeapon()
    {
        GameObject tempWeapon = weapons[0];
        for(int i = 0; i < weapons.Length;)
        {
            if (Vector3.Distance(transform.position, weapons[i].transform.position) <= Vector3.Distance(transform.position, tempWeapon.transform.position))
            {
                tempWeapon = weapons[i];
                i++;
            }

            i++;
        }

        return tempWeapon;
    }
}
