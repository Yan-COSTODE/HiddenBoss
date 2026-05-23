using System;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public string playerName;
    public double score;
    public float fTimer;
}

public class Player : Singleton<Player>
{
    [SerializeField] private PlayerData playerData;
    
    public PlayerData PlayerData => playerData;

    protected override void Awake()
    {
        base.Awake();
        playerData.score = 0;
        playerData.fTimer = 0;
    }

    private void Update()
    {
        playerData.fTimer += Time.deltaTime;
        HUD.Instance.SetTime(playerData.fTimer);
    }

    public void AddScore(double _amount)
    {
        playerData.score += _amount;
        HUD.Instance.SetScore(playerData.score);
    }

    public void SetName(string _name)
    {
        playerData.playerName = _name;
    }
}
