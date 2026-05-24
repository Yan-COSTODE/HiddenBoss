using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string playerName;
    public double score;
    public float fTimer;

    public override string ToString()
    {
        return $"{playerName} : {score} : {fTimer}s";
    }
}

public class Player : Singleton<Player>
{
    public Action OnScoreAdded;
    
    [SerializeField] private PlayerData playerData;
    private ETaskType currentTask = ETaskType.NONE;
    
    public PlayerData PlayerData => playerData;
    public ETaskType CurrentTask => currentTask;

    protected override void Awake()
    {
        base.Awake();
        playerData.score = 0;
        playerData.fTimer = 0;
    }

    private void Start()
    {
        AddScore(0);
    }

    private void OnDestroy()
    {
        OnScoreAdded = null;
    }

    private void Update()
    {
        playerData.fTimer += Time.deltaTime;
        HUD.Instance.SetTime(playerData.fTimer);
    }

    public void SetTaskType(ETaskType _taskType)
    {
        currentTask = _taskType;
    }
    
    public void AddScore(double _amount)
    {
        OnScoreAdded?.Invoke();
        playerData.score += _amount;
        HUD.Instance.SetScore(playerData.score);
    }

    public void SetName(string _name)
    {
        playerData.playerName = _name;
    }
}
