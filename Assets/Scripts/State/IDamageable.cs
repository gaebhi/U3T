using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool IsDead { get; }
    void TakeDamage(float _damage);
    Transform Transform { get; }
}