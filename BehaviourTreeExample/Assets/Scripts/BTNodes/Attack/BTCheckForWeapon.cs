using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckForWeapon : BTBaseNode
{
    VariableBoolean hasWeapon;

    public BTCheckForWeapon(VariableBoolean _hasWeapon)
    {
        hasWeapon = _hasWeapon;
    }

    public override TaskStatus Run()
    {
        if (hasWeapon.Value)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failed;
    }
}
