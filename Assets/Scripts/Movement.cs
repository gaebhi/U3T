using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour, IAction
{
    private const string STR_SPEED = "speed";

    private ActionManager m_actionManager = null;
    private NavMeshAgent m_agent = null;
    private Animator m_animator = null;

    private float m_speed = 0f;

    public void Initialize(Animator _animator, ActionManager _actionManager)
    {
        m_animator = _animator;
        m_actionManager = _actionManager;
    }

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        UpdateAnimation();
    }


    public void SetDestination(Vector3 _destination, bool _isChangeAction = true)
    {
        if (_isChangeAction)
            m_actionManager.ChangeAction(this);
        m_agent.isStopped = false;
        m_agent.destination = _destination;
    }

    private void UpdateAnimation()
    {
        Vector3 velocity = m_agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        m_speed = localVelocity.z;
        m_animator.SetFloat(STR_SPEED, m_speed);

    }

    public void Cancel()
    {
        m_agent.destination = transform.position;
        m_agent.isStopped = true;
    }
}
