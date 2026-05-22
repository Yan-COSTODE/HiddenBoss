using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private double score = 0;
    private float fTimer = 0;

    private void Update()
    {
        fTimer += Time.deltaTime;
        HUD.Instance.SetTime(fTimer);
    }

    public void AddScore(double _amount)
    {
        score += _amount;
        HUD.Instance.SetScore(score);
    }
}
