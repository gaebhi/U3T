using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private Transform m_transform = null;

    private bool m_isTriggered = false;

    private void Awake()
    {
        m_isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isTriggered && other.gameObject.CompareTag(Const.TAG_PLAYER))
        {
            m_isTriggered = true;
            Camera.main.GetComponent<CameraController>().Play(m_transform);
        }
    }
}
