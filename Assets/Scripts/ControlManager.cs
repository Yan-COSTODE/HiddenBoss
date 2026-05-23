using System;
using UnityEngine;

public class ControlManager : Singleton<ControlManager>
{
    public Action OnMouseClickPressed;
    public Action OnMouseClickReleased;
    
    [SerializeField] private KeyCode switchCamera = KeyCode.Space;
    [SerializeField] private bool bDisable = true;

    public bool Disabled => bDisable;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnMouseClickPressed?.Invoke();
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
            OnMouseClickReleased?.Invoke();
        
        if (bDisable)
            return;
        
        if (Input.GetKeyDown(switchCamera))
            CameraManager.Instance.SwitchCamera();
    }
    
    public void SetEnable(bool _status)
    {
        bDisable = !_status;
    }
}
