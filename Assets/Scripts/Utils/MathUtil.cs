using UnityEngine;

public class MathUtil : MonoBehaviour
{
    public static bool InRange(Vector3 _origin, Vector3 _target, float _range)
    {
        return Vector3.Distance(_origin, _target) < _range;
    }
}
