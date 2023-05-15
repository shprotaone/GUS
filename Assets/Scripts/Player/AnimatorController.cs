using GUS.Core.Locator;
using GUS.Player.State;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int RunStateId = Animator.StringToHash("Run");
    private int JumpId = Animator.StringToHash("Jump");

    public void RunActivate(bool flag)
    {
        _animator.SetBool(RunStateId, flag);
    }

    public void JumpActivate()
    {
        _animator.SetTrigger(JumpId);
    }

}
