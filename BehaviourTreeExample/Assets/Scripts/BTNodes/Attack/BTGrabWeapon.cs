using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTGrabWeapon : BTBaseNode
{
    VariableGameObject weaponToGrab;
    Transform holdTransform;
    VariableBoolean hasWeapon;

    public BTGrabWeapon(VariableBoolean _hasWeapon, VariableGameObject _weaponToGrab, Transform _holdTransform)
    {
        weaponToGrab = _weaponToGrab;
        holdTransform = _holdTransform;
        hasWeapon = _hasWeapon;
    }

    public override TaskStatus Run()
    {
        //weaponToGrab.Value.transform.position = holdTransform.position;
        //weaponToGrab.Value.transform.parent = holdTransform;
        hasWeapon.Value = true;
        return TaskStatus.Success;
    }
}
