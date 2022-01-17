using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private ActionManager m_actionManager = null;
    private Animator m_animator = null;

    private Death m_health = null;

    private void Awake()
    {
        m_actionManager = GetComponent<ActionManager>();
        m_animator = GetComponent<Animator>();

        m_health = GetComponent<Death>();
    }

    private void Start()
    {
        m_health.Initialize(m_animator, m_actionManager);
    }
}
