using UnityEngine;
public class StateMachine : MonoBehaviour
{
    private IState m_currentState = null;

    public void ChangeState(IState _state)
    {
        if(m_currentState != null && m_currentState != _state)
            m_currentState.Cancel();
        m_currentState = _state;
    }

    public void CancelState()
    {
        if (m_currentState != null)
        {
            m_currentState.Cancel();
            m_currentState = null;
        }
    }
}
