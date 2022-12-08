using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckForWeapon : BTBaseNode
{
    private bool hasWeapon = false;

    public BTCheckForWeapon(VariableBoolean _hasWeapon)
    {
        hasWeapon = _hasWeapon.Value;
    }

    public override TaskStatus Run()
    {
        if (hasWeapon)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failed;
    }
}
