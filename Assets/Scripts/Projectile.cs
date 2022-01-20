using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{

    public EProjectile Type;
    [SerializeField] private float m_speed = 1f;
    [SerializeField] private GameObject m_hitEffect = null;

    private IDamageable m_target = null;
    private float m_damage = 5f;
    private bool m_isGuide = true;

    void Update()
    {
        if (m_target == null)
            return;
        if (m_isGuide && m_target.IsDead == false)
            transform.LookAt(GetTargetPosition());
        transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
    }

    public void SetTargetAndDamage(IDamageable _target, float _damage, bool _isGuide = true)
    {
        m_target = _target;
        m_damage = _damage;
        m_isGuide = _isGuide;
        transform.LookAt(GetTargetPosition());

        DOTween.To(() => 0, (float _value) => { }, 1, 10)
            .SetTarget(this.transform)
            .OnComplete(() =>
            {
                if (gameObject.activeInHierarchy)
                    ProjectileObjectPool.Instance.Push(this);
            });
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x = m_target.Transform.position.x;
        targetPosition.z = m_target.Transform.position.z;
        return targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != m_target)
            return;
        if (m_target.IsDead)
            return;
        m_target.TakeDamage(m_damage);

        if (m_hitEffect != null)
        {
            GameObject effect = Instantiate(m_hitEffect, GetTargetPosition(), transform.rotation);
            DOTween.To(() => 0, (float _value) => { }, 1, 10)
                .SetTarget(this.transform)
                .OnComplete(() =>
                {
                    if (effect.activeInHierarchy)
                        effect.SetActive(false);
                });
        }

        ProjectileObjectPool.Instance.Push(this);
    }
}
