using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    private const float RADIUS = 0.5f;
    private int m_index = 0;

    private void Awake()
    {
        m_index = 0;
    }

    public Transform GetCurrentWayPoint()
    {
        return transform.GetChild(m_index).transform;
    }

    public Transform GetNextWayPoint()
    {
        m_index = (m_index + 1) % transform.childCount;
        return transform.GetChild(m_index).transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < transform.childCount; ++i)
        {
            Gizmos.DrawSphere(GetCurrentWayPoint().position, RADIUS);
            Gizmos.DrawLine(GetCurrentWayPoint().position, GetNextWayPoint().position);
        }
    }
}
