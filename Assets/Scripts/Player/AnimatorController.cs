using DG.Tweening;
using GUS.Core.Locator;
using GUS.Player.State;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int RunStateId = Animator.StringToHash("Run");
    private int JumpId = Animator.StringToHash("Jump");
    private int CrouchID = Animator.StringToHash("Crouch");

    private float _prevSpeed;
    public void RunActivate(bool flag) => _animator.SetBool(RunStateId, flag);
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

}
