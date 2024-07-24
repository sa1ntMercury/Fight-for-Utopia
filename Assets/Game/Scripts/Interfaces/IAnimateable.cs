using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimateable
{
    public Animator Animator { get; }

    public bool IsShoot { get; }
    public bool IsWalk { get;  }
    public bool IsRun { get;  }
    public bool IsJump { get; }
    public bool IsFall { get; }
    public bool IsCrouch { get; }
    public bool IsDead { get; }

    public void AnimationSetter();

}
    

