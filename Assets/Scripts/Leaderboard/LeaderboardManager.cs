using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TopScore
{
    public List<PlayerData> playerDataList = new List<PlayerData>();
}

public class LeaderboardManager : Singleton<LeaderboardManager>
{
    public void SaveData(TopScore topScore)
    {
        String newScoreToRegister = JsonUtility.ToJson(topScore);
        PlayerPrefs.SetString("LeaderboardSave", newScoreToRegister);
        PlayerPrefs.Save();
    }

    public TopScore LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey("LeaderboardSave"))
        {
            String leadboard = PlayerPrefs.GetString("LeaderboardSave");
            return JsonUtility.FromJson<TopScore>(leadboard);
        }
            
        return new TopScore();
    }

    public void AddScoreToLeaderBoard(PlayerData newScore)
    {
        TopScore currentTopScore = LoadLeaderboard();

        int _found = currentTopScore.playerDataList.FindIndex(x => x.playerName == newScore.playerName);
        
        if (_found >= 0 && currentTopScore.playerDataList[_found].score > newScore.score)
            return;
        
        currentTopScore.playerDataList.Add(newScore);
        currentTopScore.playerDataList.Sort((a, b) => b.score.CompareTo(a.score));
        
        while (currentTopScore.playerDataList.Count > 5)
            currentTopScore.playerDataList.RemoveAt(currentTopScore.playerDataList.Count - 1);
        
        SaveData(currentTopScore);
    }
}