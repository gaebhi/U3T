using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float RAY_DISTANCE = 1000;

    private ActionManager m_actionManager = null;
    private Animator m_animator = null;

    private Movement m_movement = null;
    private Attack m_attack = null;
    private Death m_death= null;

    private void Awake()
    {
        m_actionManager = GetComponent<ActionManager>();
        m_animator = GetComponent<Animator>();

        m_attack = GetComponent<Attack>();
        m_movement = GetComponent<Movement>();
        m_death = GetComponent<Death>();
    }

    private void Start()
    {
        m_attack.Initialize(m_animator, m_actionManager);
        m_movement.Initialize(m_animator, m_actionManager);
        m_death.Initialize(m_animator, m_actionManager);
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        Ray ray = GetScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, RAY_DISTANCE);
            float[] distances = new float[hits.Length];
            Array.Sort(distances, hits);

            foreach (RaycastHit hit in hits)
            {
                IDamageable target = hit.transform.GetComponent<IDamageable>();
                if (target != null && !target.IsDead)
                {
                    if (target.GetTransform() != transform)
                        m_attack.SetTarget(target);
                    return;
                }
            }

            bool isHit = Physics.Raycast(ray, out RaycastHit outHit, RAY_DISTANCE);
            if (isHit)
            {
                m_movement.SetDestination(outHit.point);
            }
        }
    }

    private Ray GetScreenPointToRay(Vector3 _position)
    {
        return Camera.main.ScreenPointToRay(_position);
    }
}
