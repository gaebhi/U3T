using UnityEngine;
public interface IAction
{
    void Initialize(Animator _animator, ActionManager _actionManager);
    void Cancel();
}
