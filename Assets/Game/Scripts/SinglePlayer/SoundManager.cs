using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerManager
{
    private BasePlayer _player;

   

    //[SerializeField] private AudioClip _Idle;
    //[SerializeField] private AudioClip _Walk;
    //[SerializeField] private AudioClip _Run;
    //[SerializeField] private AudioClip _Jump;
    //[SerializeField] private AudioClip _Fall;
    //[SerializeField] private AudioClip _Crouch;
    //[SerializeField] private AudioClip _Shoot;
    //[SerializeField] private AudioClip _Dead;


    private SoundEnum _currentState;

    public SoundPlayerManager(BasePlayer player)
    {
        _player = player;
    }

    public void PlaySound(SoundEnum soundState, bool active) //Enum + условие
    {
        if (soundState < _currentState)
            return;

        if (!active)                             
        {
            if (soundState == _currentState)
            {
                _player.AudioSource.clip = _player._audioClips[(int)SoundEnum.Idle];
                _currentState = SoundEnum.Idle;
            }

            return;
        }

        _player.AudioSource.clip = _player._audioClips[(int)soundState];
        _player.AudioSource.Play();
        _currentState = soundState;
    }

    public void SoundStateMachine()
    {
        PlaySound(SoundEnum.Walk, _player.IsWalk);
        PlaySound(SoundEnum.Run, _player.IsRun);
        PlaySound(SoundEnum.Jump, _player.IsJump);
        PlaySound(SoundEnum.Fall, _player.IsFall);
        PlaySound(SoundEnum.Crouch, _player.IsCrouch);
        PlaySound(SoundEnum.Shoot, _player.IsShoot);
        PlaySound(SoundEnum.Dead, _player.IsDead);
    }
}
