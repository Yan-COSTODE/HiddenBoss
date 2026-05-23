using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button windowsButton;
    [SerializeField] private TMP_InputField nameText;
    [SerializeField] private LeaderboardItem leaderboardItem;
    [SerializeField] private Transform leaderboardContainer;

    private void Awake()
    {
        quitButton.onClick.AddListener(Quit);
        playButton.onClick.AddListener(Play);
        nameText.onValueChanged.AddListener(UpdateName);
    }

    private void Start()
    {
        ShowLeaderboard();
        Player.Instance.SetName(IDGenerator.GenerateID("Player_"));
    }

    private void UpdateName(string _name)
    {
        Player.Instance.SetName(name);
    }
    
    private void Play()
    {
        Debug.Log("Play");
        gameObject.SetActive(false);
        windowsButton.interactable = false;
    }
    
    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
        LeaderboardManager.Instance.AddScoreToLeaderBoard(Player.Instance.PlayerData);
    }
    
    private void ShowLeaderboard()
    {
        TopScore _topScore = LeaderboardManager.Instance.LoadLeaderboard();

        foreach (PlayerData _data in _topScore.playerDataList)
            Instantiate(leaderboardItem, leaderboardContainer).Init(_data);
    }
}
