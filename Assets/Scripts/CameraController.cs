using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Transform m_target = null;

    void LateUpdate()
    {
        transform.position = m_target.position;
    }
}
