using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine m_stateMachine = null;
    protected Animator m_animator = null;

    protected virtual void Awake()
    {
        m_stateMachine = GetComponent<StateMachine>();
        m_animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        //initialize action
    }
}
