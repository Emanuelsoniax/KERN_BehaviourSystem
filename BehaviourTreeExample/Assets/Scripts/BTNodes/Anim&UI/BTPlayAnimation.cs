using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayAnimation : BTBaseNode
{
    private Animator animator;
    private string animName;

    public BTPlayAnimation(Animator _animator, string _animName)
    {
        animator = _animator;
        animName = _animName;
    }

    public override TaskStatus Run()
    {
        animator.Play(animName);
        return TaskStatus.Success;
    }
}
