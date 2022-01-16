using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private const string STR_SPEED = "speed";
    private Ray m_ray;
    private NavMeshAgent m_agent = null;
    private Animator m_animator = null;

    private float m_speed = 0f;
    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move();
        }
        UpdateAnimator();
    }
    private void Move()
    {
        m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(m_ray, out hit);
        if (isHit)
        {
            m_agent.destination = hit.point;
        }
        
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = m_agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        m_speed = localVelocity.z;
        m_animator.SetFloat(STR_SPEED, m_speed);

    }
    
}
