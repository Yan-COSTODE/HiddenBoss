using TMPro;
using UnityEngine;

public class Clock : Singleton<Clock>
{
    private const int MAX_TIME = 86400;
    [SerializeField] private float fFactor = 144;
    [SerializeField, Range(0, MAX_TIME)] private float fTime = 28800.0f;
    [SerializeField] private Transform hoursPin;
    [SerializeField] private Transform minutesPin;
    [SerializeField] private Transform secondsPin;

    public float CurrentTime => fTime;
    
    private void Update()
    {
        UpdateTime();
        UpdateClock();
    }

    private void UpdateTime()
    {
        fTime += Time.deltaTime * fFactor;
        
        if (fTime > MAX_TIME)
            fTime -= MAX_TIME;
    }
    
    private void UpdateClock()
    {
        Vector3Int _time = GetTime();
        hoursPin.localEulerAngles = new Vector3(0.0f, GetRotationHours(0, 24, _time.x), 0.0f);
        minutesPin.localEulerAngles = new Vector3(0.0f, GetRotation(0, 60, _time.y), 0.0f);
        secondsPin.localEulerAngles = new Vector3(0.0f, GetRotation(0, 60, _time.z), 0.0f);
    }

    private float GetRotation(float _min, float _max, float _value)
    {
        return (_value - _min) / (_max - _min) * 360.0f;
    }
    
    private float GetRotationHours(float _min, float _max, float _value)
    {
        return (_value - _min) / (_max - _min) * 720.0f;
    }
    
    public Vector3Int GetTime()
    {
        Vector3Int _time = Vector3Int.zero;
        _time.x = Mathf.FloorToInt(fTime / 3600);
        _time.y = Mathf.FloorToInt((fTime % 3600) / 60);
        _time.z = Mathf.FloorToInt(fTime % 60);
        return _time;
    }
}
