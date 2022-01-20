using UnityEngine;
using DG.Tweening;
using UnityEditor;

[CustomEditor(typeof(DayNight))]
public class DayNightEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DayNight dayNight = (DayNight)target;
        if (GUILayout.Button("Change"))
        {
            dayNight.ChangeDay();
        }

    }
}
public class DayNight : MonoBehaviour
{
    private Light m_directionalLight = null;
    private const float CONST_24OURS = 24f;
    
    [SerializeField] private Gradient m_directionalColor;
    [SerializeField] private Gradient m_fogColor;
    [SerializeField, Range(0, 24)] private float m_time = 0f;

    private void Awake()
    {
        m_directionalLight = GetComponent<Light>();
    }
    
    
    public void ChangeDay(float _duration = 3f)
    {
        DOVirtual.Float(m_time, m_time + CONST_24OURS*0.5f, _duration, (float _value) => { m_time = _value%CONST_24OURS; });
    }

    private void Update()
    {
        UpdateLighting(m_time/CONST_24OURS);
    }

    private void UpdateLighting(float _value)
    {
        RenderSettings.fogColor = m_fogColor.Evaluate(_value);

        if (m_directionalLight != null)
        {
            m_directionalLight.color = m_directionalColor.Evaluate(_value);
            m_directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((_value * 360f) + 120f, (_value * 360f) + 60f, 0f));
        }

    }
}