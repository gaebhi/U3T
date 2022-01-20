using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create ScriptableObject/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private AnimatorOverrideController m_animator = null;

    [SerializeField] private GameObject m_weaponPrefab = null;

    [SerializeField] private bool m_isRightHand = true;

    [SerializeField] private EProjectile m_projectileType;

    [SerializeField] private bool m_isGuide = true;

    public float CoolTime = 1f;
    public float Range = 1.5f;
    public float Damage = 10f;

    public void EquipWeapon(Transform _right, Transform _left, Animator _animator)
    {
        DestroyWeapon(_right, _left);
        if (m_weaponPrefab != null)
        {
            GameObject weapon = Instantiate(m_weaponPrefab, GetTransform(_right, _left));
            weapon.name = Const.STR_WEAPON;
        }
        if (m_animator != null)
            _animator.runtimeAnimatorController = m_animator;
    }

    private Transform GetTransform(Transform _right, Transform _left)
    {
        if (m_isRightHand)
            return _right;
        return _left;
    }

    public bool HasProjectile()
    {
        return m_projectileType != EProjectile.NONE;
    }

    public void ShootProjectile(Transform _right, Transform _left, IDamageable _target)
    {
        Projectile projectile = ProjectileObjectPool.Instance.Pop(m_projectileType);
        if (projectile != null)
        {
            projectile.transform.position = GetTransform(_right, _left).position;
            projectile.gameObject.SetActive(true);
            projectile.SetTargetAndDamage(_target, Damage, m_isGuide);
        }
    }

    private void DestroyWeapon(Transform _right, Transform _left)
    {
        Transform weapon = _right.Find(Const.STR_WEAPON);
        if (weapon != null)
            Destroy(weapon.gameObject);
        weapon = _left.Find(Const.STR_WEAPON);
        if (weapon != null)
            Destroy(weapon.gameObject);

    }
}
