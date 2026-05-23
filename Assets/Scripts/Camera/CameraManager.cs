using System;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public Action OnGoToBase;
    public Action OnGoToScreen;
    public Action OnCompletedToBase;
    public Action OnCompletedToScreen;
    
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform computerScreenPosition;
    [SerializeField] private Transform baseCameraPosition;
    [Header("Settings")] 
    [SerializeField, Range(0, 10)] private float fTransitionTime = 1.0f;
    [SerializeField] private EEasing transitionEasing = EEasing.EASE_NONE;
    private bool bMoving;
    private bool bScreen;

    protected override void Awake()
    {
        base.Awake();
        OnGoToBase = () => SetBool(true, false);
        OnGoToScreen = () => SetBool(true, true);
        OnCompletedToBase = () => SetBool(false, false);
        OnCompletedToScreen = () => SetBool(false, true);
        mainCamera.transform.position = computerScreenPosition.position;
        mainCamera.transform.rotation = computerScreenPosition.rotation;
        SetBool(false, true);
    }

    private void OnDestroy()
    {
        OnGoToBase = null;
        OnGoToScreen = null;
        OnCompletedToBase = null;
        OnCompletedToScreen = null;
    }

    private void SetBool(bool _moving, bool _screen)
    {
        bMoving = _moving;
        bScreen = _screen;
    }

    public void SwitchCamera()
    {
        if (bMoving)
            return;
        
        if (bScreen)
            MoveHelper(baseCameraPosition, OnGoToBase, OnCompletedToBase);
        else
            MoveHelper(computerScreenPosition, OnGoToScreen, OnCompletedToScreen);
    }

    private void MoveHelper(Transform _target, Action _start, Action _complete)
    {
        mainCamera.transform.DOMove(_target.position, fTransitionTime, transitionEasing);
        mainCamera.transform.DORotate(_target.rotation, fTransitionTime, transitionEasing);
        TimerManager.Instance.Create(fTransitionTime, transitionEasing, _complete, _start);
    }
}
