using System;
using TMPro;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;

    public void SetTime(float _time)
    {
        _time += Time.deltaTime;
        timeText.text = $"{Mathf.FloorToInt(_time / 60):00}:{_time % 60:00}";
    }
    
    public void SetScore(double _score)
    {
        scoreText.text = _score.ToString("N0");
    }
}
