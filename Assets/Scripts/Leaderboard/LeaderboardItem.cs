using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public void Init(PlayerData _playerData)
    {
        nameText.text = _playerData.playerName;
        timeText.text = _playerData.fTimer.ToString("N0") + "s";
        scoreText.text = HUD.FormatScore(_playerData.score);
    }
}
