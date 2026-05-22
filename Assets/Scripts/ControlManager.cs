using UnityEngine;

public class ControlManager : Singleton<ControlManager>
{
    [SerializeField] private KeyCode switchCamera = KeyCode.Space;
    [SerializeField] private KeyCode addScore = KeyCode.A;

    private void Update()
    {
        if (Input.GetKeyDown(switchCamera))
            CameraManager.Instance.SwitchCamera();
        if (Input.GetKey(addScore))
            Player.Instance.AddScore(100.5);
    }
}
