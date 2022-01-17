using UnityEngine;
public class ActionManager : MonoBehaviour
{
    private IAction m_currentAction = null;

    public void ChangeAction(IAction _action)
    {
        if(m_currentAction != null && m_currentAction != _action)
            m_currentAction.Cancel();
        m_currentAction = _action;
    }

    public void CancelAction()
    {
        if (m_currentAction != null)
        {
            m_currentAction.Cancel();
            m_currentAction = null;
        }
    }
}
