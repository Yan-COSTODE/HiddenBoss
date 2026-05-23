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
        string _raw = _playerData.score.ToString("e3");
        string[] _parts = _raw.Split("e");
        int _power = int.Parse(_parts[1]);
        scoreText.text = $"{_parts[0]}<size=50%>x</size>10<sup>{_power}</sup>";
    }
}
