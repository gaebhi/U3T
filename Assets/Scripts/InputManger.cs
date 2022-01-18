using UnityEngine;
using System;

public enum EInputType
{
    Began,
    Cancled,
    Ended,
    Moved,
    Stationary
}
public class InputManager : Singleton<InputManager>
{
    public Action<EInputType, Vector2> OnInput = null;

    private void Update()
    {
#if UNITY_EDITOR
        handleMouse();
#elif UNITY_ANDROID || UNITY_IPHONE
        handleTouch();
#endif
    }
    private void handleMouse()
    {
        if (Input.GetMouseButton(0))
        {
            if (OnInput != null)
                OnInput(EInputType.Began, Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (OnInput != null)
                OnInput(EInputType.Stationary, Input.mousePosition);
        }

    }

    private void handleTouch()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (OnInput != null)
                OnInput(EInputType.Began, touch.position);
        }
        if (touch.phase == TouchPhase.Stationary)
        {
            if (OnInput != null)
                OnInput(EInputType.Stationary, touch.position);
        }
    }
}
