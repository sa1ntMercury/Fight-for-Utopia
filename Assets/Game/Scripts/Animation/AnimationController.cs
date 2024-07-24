using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    private IAnimateable _player;
    private AnimationEnum _currentState;
    private string _animatorParameterName;


    public AnimationController(IAnimateable player)
    {
        _player = player;
        _animatorParameterName = player.Animator.GetParameter(index: 0).name;
    }

    public void PlayAnimation(AnimationEnum animationState, bool active) //Enum + условие
    {
        if (animationState < _currentState)
            return;

        if (!active)
        {
            if (animationState == _currentState)
            {
                _player.Animator.SetInteger(_animatorParameterName, (int)AnimationEnum.Idle);
                _currentState = AnimationEnum.Idle;
            }

            return;
        }

        _player.Animator.SetInteger(_animatorParameterName, (int)animationState);
        _currentState = animationState;
    }

    public void AnimationStateMachine()
    {
        PlayAnimation(AnimationEnum.Walk, _player.IsWalk);
        PlayAnimation(AnimationEnum.Run, _player.IsRun);
        PlayAnimation(AnimationEnum.Jump, _player.IsJump);
        PlayAnimation(AnimationEnum.Fall, _player.IsFall);
        PlayAnimation(AnimationEnum.Crouch, _player.IsCrouch);
        PlayAnimation(AnimationEnum.Shoot, _player.IsShoot);
        PlayAnimation(AnimationEnum.Dead, _player.IsDead);
    }
}

