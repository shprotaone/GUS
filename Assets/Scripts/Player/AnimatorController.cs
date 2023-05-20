using DG.Tweening;
using GUS.Core.Locator;
using GUS.Player;
using GUS.Player.State;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleController _particleController;

    private int RunStateId = Animator.StringToHash("Run");
    private int JumpId = Animator.StringToHash("Jump");
    private int CrouchID = Animator.StringToHash("Crouch");
    private int RunSpeedMultiply = Animator.StringToHash("RunMultiplyer");
    private int DeathId = Animator.StringToHash("Death");

    private float _prevSpeed;
    public void RunActivate(bool flag) => _animator.SetBool(RunStateId, flag);
    public void DeathActivate() => _animator.SetTrigger(DeathId);
    public void AfterDeath() => _particleController.AfterDeath();
    public void JumpActivate() => _animator.SetTrigger(JumpId);
    public void CrouchActivate() => _animator.SetTrigger(CrouchID);
    public void Pause(bool flag)
    {
        if (flag)
        {
            _prevSpeed = _animator.speed;
            _animator.speed = 0;
        }
        else
        {
            _animator.speed = _prevSpeed;
        }
    }
    public void ChangeAnimationSpeed(float speed)
    {
        _animator.SetFloat(RunSpeedMultiply, speed / 10);
    }

}
