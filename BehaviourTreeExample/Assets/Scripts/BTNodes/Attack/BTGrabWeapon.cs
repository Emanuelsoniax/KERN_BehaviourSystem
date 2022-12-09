using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGrabWeapon : BTBaseNode
{

    private VariableBoolean hasWeapon;

    public BTGrabWeapon(VariableBoolean _hasWeapon)
    {
        hasWeapon = _hasWeapon;
    }

    public override TaskStatus Run()
    {
        hasWeapon.Value = true;
        return TaskStatus.Success;
    }
}
