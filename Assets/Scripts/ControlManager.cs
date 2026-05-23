using UnityEngine;

public class ControlManager : Singleton<ControlManager>
{
    [SerializeField] private KeyCode switchCamera = KeyCode.Space;
    [SerializeField] private bool bDisable = true;

    public bool Disabled => bDisable;
    
    private void Update()
    {
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
