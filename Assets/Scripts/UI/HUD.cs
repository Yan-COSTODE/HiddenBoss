using System;
using TMPro;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text[] scoreText;

    private void Start()
    {
        //SetVisibility(false);
    }

    public void SetVisibility(bool _status)
    {
        gameObject.SetActive(_status);
    }
    
    public void SetTime(float _time)
    {
        timeText.text = $"{Mathf.FloorToInt(_time / 60):00}:{_time % 60:00}";
    }
    
    public void SetScore(double _score)
    {
        foreach (TMP_Text _text in scoreText)
            _text.text = FormatScore(_score);
    }

    public static string FormatScore(double _score)
    {
        if (_score < 1000000.0)
            return $"{_score:N0}";
        
        string _raw = _score.ToString("e3");
        string[] _parts = _raw.Split("e");
        int _power = int.Parse(_parts[1]);
        return $"{_parts[0]}<size=50%>x</size>10<sup>{_power}</sup>";
    }
}
