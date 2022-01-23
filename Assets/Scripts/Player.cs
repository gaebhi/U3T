using System;
using UnityEngine;

public class Player : Entity
{
    private const float RAY_DISTANCE = 1000;

    private Movement m_movement = null;
    private Attack m_attack = null;
    private Death m_death= null;

    protected override void Awake()
    {
        base.Awake();

        m_attack = GetComponent<Attack>();
        m_movement = GetComponent<Movement>();
        m_death = GetComponent<Death>();
    }

    protected override void Start()
    {
        base.Start();

        m_attack.Initialize(m_animator, m_stateMachine);
        m_movement.Initialize(m_animator, m_stateMachine);
        m_death.Initialize(m_animator, m_stateMachine);

        InputManager.Instance.OnInput = OnInput;
    }

    private void Update()
    {
        if (m_death.IsDead)
        {
            InputManager.Instance.OnInput = null;
            return;
        }
    }

    public void OnInput(EInputType _action, Vector2 _inputPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(_inputPosition);

        if (_action == EInputType.Began)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, RAY_DISTANCE);
            float[] distances = new float[hits.Length];
            Array.Sort(distances, hits);

            foreach (RaycastHit hit in hits)
            {
                IDamageable target = hit.transform.GetComponent<IDamageable>();
                if (target != null && !target.IsDead)
                {
                    if (target.Transform != transform)
                        m_attack.SetTargetAndChangeAction(target);
                    return;
                }
            }

            bool isHit = Physics.Raycast(ray, out RaycastHit outHit, RAY_DISTANCE);
            if (isHit)
            {
                m_movement.SetDestinationAndChangeAction(outHit.point);
            }
        }
    }

    public void CancelCurrentAction()
    {
        m_stateMachine.CancelState();
    }
}
