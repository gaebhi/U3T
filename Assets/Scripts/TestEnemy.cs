using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Entity
{
    [SerializeField] private PatrolPath m_path = null;
    [SerializeField, Range(0, 1)] private float m_speedMulti = 0.2f;

    private const float CHASE_RANGE = 8f;
    private const float WAYPOINT_RANGE = 1f;
    private const float WAIT_TIME = 2f;

    private Movement m_movement = null;
    private Attack m_attack = null;
    private Death m_death = null;

    private IDamageable m_target = null;

    private Vector3 m_initPosition;
    private float m_elapsedWaitTime;
    private float m_elapsedWaypointWaitTime;

    protected override void Awake()
    {
        base.Awake();

        m_attack = GetComponent<Attack>();
        m_movement = GetComponent<Movement>();
        m_death = GetComponent<Death>();

        m_target = GameObject.FindWithTag(Const.TAG_PLAYER).GetComponent<IDamageable>();
    }

    protected override void Start()
    {
        base.Start();

        m_attack.Initialize(m_animator, m_actionManager);
        m_movement.Initialize(m_animator, m_actionManager);
        m_death.Initialize(m_animator, m_actionManager);

        m_initPosition = transform.position;

        m_elapsedWaitTime = 0f;
        m_elapsedWaypointWaitTime = 0f;
    }

    private void Update()
    {
        if (m_death.IsDead)
        {
            DeathState();
        }

        else if (!m_target.IsDead && MathUtil.InRange(transform.position, m_target.Transform.position, CHASE_RANGE))
        {
            AttackState();
        }

        else if (!m_target.IsDead && m_elapsedWaitTime < WAIT_TIME)
        {
            WaitState();
        }

        else
        {
            PatrolState();
        }

        m_elapsedWaitTime += Time.deltaTime;
        m_elapsedWaypointWaitTime += Time.deltaTime;
    }

    private void DeathState()
    {
        m_attack.Cancel();
        m_movement.Cancel();
        m_actionManager.CancelAction();
    }

    private void AttackState()
    {
        m_elapsedWaitTime = 0f;
        m_elapsedWaypointWaitTime = 0f;
        m_attack.SetTargetAndChangeAction(m_target);
    }

    private void WaitState()
    {
        m_attack.Cancel();
        m_movement.Cancel();
        m_actionManager.CancelAction();
    }

    private void PatrolState()
    {
        Vector3 nextPosition = m_initPosition;

        if (m_path != null)
        {
            if (MathUtil.InRange(transform.position, m_path.GetCurrentWayPoint().position, WAYPOINT_RANGE))
            {
                m_elapsedWaypointWaitTime = 0f;
                nextPosition = m_path.GetNextWayPoint().position;
            }
            else
            {
                nextPosition = m_path.GetCurrentWayPoint().position;
            }
        }
        if (m_elapsedWaypointWaitTime > WAIT_TIME)
        {
            m_movement.SetDestinationAndChangeAction(nextPosition, m_speedMulti);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CHASE_RANGE);
    }
}
