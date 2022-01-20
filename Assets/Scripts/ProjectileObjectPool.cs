using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EProjectile
{
    NONE,
    FIREBALL
}

public static class EProjectileExtentions
{
    public static string EnumToString(this EProjectile _type)
    {
        string result = string.Empty;

        switch (_type)
        {
            case EProjectile.NONE:
                result = "FireballProjectile";
                break;
            case EProjectile.FIREBALL:
                result = "FireballProjectile";
                break;
            default:
                result = "FireballProjectile";
                break;
        }
        return result;
    }
}

public class ProjectileObjectPool : Singleton<ProjectileObjectPool>
{
    private Dictionary<EProjectile, List<Projectile>> m_objectPool;

    private void Awake()
    {
        m_objectPool = new Dictionary<EProjectile, List<Projectile>>();
    }

    private void OnDestroy()
    {
        m_objectPool.Clear();
    }

    public Projectile Pop(EProjectile _type)
    {
        Projectile result = null;

        if (m_objectPool.ContainsKey(_type))
        {
            List<Projectile> projectiles = m_objectPool[_type];

            result = projectiles[0];
            projectiles.RemoveAt(0);
            if (projectiles.Count < 1)
            {
                m_objectPool.Remove(_type);
            }
        }
        else
        {
            result = Resources.Load<Projectile>(Const.PATH_PROJECTILE + _type.EnumToString());
            result = Instantiate(result);
        }
        
        //result.gameObject.SetActive(true);
        return result;
    }

    public void Push(Projectile _projectile)
    {
        if (m_objectPool.ContainsKey(_projectile.Type))
        {
            List<Projectile> projectiles = m_objectPool[_projectile.Type];
            projectiles.Add(_projectile);
        }
        else
        {
            List<Projectile> projectiles = new List<Projectile>();
            projectiles.Add(_projectile);
            m_objectPool.Add(_projectile.Type, projectiles);
        }
        _projectile.gameObject.SetActive(false);
    }
}
