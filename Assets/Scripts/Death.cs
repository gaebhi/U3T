using System;
using UnityEngine;

public class Death : MonoBehaviour, IDamageable, IAction
{
    private const string STR_TRIGGER = "die";

    public bool IsDead 
    {
        get { return m_isDead; }
    }
    private bool m_isDead = false;

    [SerializeField] private float m_maxHealth;
    private float m_currentHealth;
    private Animator m_animator;
    private ActionManager m_actionManager;

    public void Initialize(Animator _animator, ActionManager _actionManager)
    {
        m_currentHealth = m_maxHealth;
        m_isDead = false;

        m_animator = _animator;
        m_actionManager = _actionManager;
    }

    public void TakeDamage(float _damage)
    {
        if (m_isDead)
            return;

        m_currentHealth = Mathf.Clamp(m_currentHealth - _damage, 0, m_maxHealth);
        Debug.Log(m_currentHealth);
        if (m_currentHealth <= 0)
        {
            m_isDead = true;
            m_animator.SetTrigger(STR_TRIGGER);
            m_actionManager.CancelAction();
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Cancel()
    {
        
    }
}
