using UnityEngine;
public interface IState
{
    void Initialize(Animator _animator, StateMachine _stateMachine);
    void Cancel();
}
