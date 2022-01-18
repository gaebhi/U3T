using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected ActionManager m_actionManager = null;
    protected Animator m_animator = null;

    protected virtual void Awake()
    {
        m_actionManager = GetComponent<ActionManager>();
        m_animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        //initialize action
    }
}
