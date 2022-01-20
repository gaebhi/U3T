using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IAction
{
    private const string STR_TRIGGER = "attack";
    private const string STR_STOP_TRIGGER = "stopAttack";

    [SerializeField] private float m_attackRange = 2f;
    [SerializeField] private float m_coolTime = 1.5f;
    [SerializeField] private float m_weaponDamage = 10f;
    [SerializeField] private GameObject m_weaponPrefab = null;
    [SerializeField] private Transform m_handTransform = null;

    private Transform m_target = null;
    private Vector3 m_targetPositon = Vector3.zero;

    private Animator m_animator = null;
    private ActionManager m_actionManager = null;

    private float m_elapsedTime = 0f;

    public void Initialize(Animator _animator, ActionManager _actionManager)
    {
        m_elapsedTime = 0f;
        m_animator = _animator;
        m_actionManager = _actionManager;
    }

    private void Update()
    {
        m_elapsedTime += Time.deltaTime;

        if (m_target == null)
            return;

        if (!MathUtil.InRange(transform.position, m_targetPositon, m_attackRange) && m_elapsedTime > m_coolTime)
        {
            GetComponent<Movement>().SetDestination(m_target.transform.position);
        }
        else
        {
            if (m_target.GetComponent<IDamageable>().IsDead)
                return;
            GetComponent<Movement>().Cancel();
            AttackTrigger();
        }
    }

    public void SetTargetAndChangeAction(IDamageable _damageable)
    {
        m_actionManager.ChangeAction(this);
        m_target = _damageable.Transform;
        m_targetPositon = m_target.position;
        m_targetPositon.y = 0f;
    }

    public void AttackTrigger()
    {
        transform.LookAt(m_targetPositon);

        if (m_elapsedTime > m_coolTime)
        {
            m_elapsedTime = 0f;
            m_animator.ResetTrigger(STR_STOP_TRIGGER);
            m_animator.SetTrigger(STR_TRIGGER);
        }
    }

    public void Cancel()
    {
        if (m_target != null)
            m_target = null;
        m_animator.ResetTrigger(STR_TRIGGER);
        m_animator.SetTrigger(STR_STOP_TRIGGER);
    }

    //aimation event
    private void Hit()
    {
        if(m_target != null)
            m_target.GetComponent<Death>().TakeDamage(50f);
    }
}
