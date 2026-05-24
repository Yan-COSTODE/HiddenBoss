using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour
{
    public Action onBet;
    public Action onWin;
    public Action onLose;
    
    [SerializeField] private RectTransform rouletteImage;
    [SerializeField] private Button redButton;
    [SerializeField] private Button blackButton;
    [SerializeField] private float fAnimationTime = 3.0f;
    [SerializeField] private float fAnimationSpeed = 3.0f;
    [SerializeField] private Image coinsImage;
    [SerializeField] private Sprite redCoinSprite;
    [SerializeField] private Sprite blackCoinSprite;
    private double fScoreIn = 0.0f;
    private Color scoreIn = Color.white;

    private void Awake()
    {
        redButton.onClick.AddListener(RedButton);
        blackButton.onClick.AddListener(BlackButton);
    }

    private void OnDestroy()
    {
        onBet = null;
        onWin = null;
        onLose = null;
    }

    private void RedButton()
    {
        scoreIn = Color.red;
        fScoreIn = Player.Instance.PlayerData.score;
        Player.Instance.AddScore(-fScoreIn);
        coinsImage.sprite = redCoinSprite;
        coinsImage.gameObject.SetActive(true);
        LaunchRoulette();
    }
    
    private void BlackButton()
    {
        scoreIn = Color.black;
        fScoreIn = Player.Instance.PlayerData.score;
        Player.Instance.AddScore(-fScoreIn);
        coinsImage.sprite = blackCoinSprite;
        coinsImage.gameObject.SetActive(true);
        LaunchRoulette();
    }

    private void LaunchRoulette()
    {
        onBet?.Invoke();
        GetComponentInParent<GameWindows>().CloseButton.interactable = false;
        redButton.interactable = false;
        blackButton.interactable = false;
        TimerManager.Instance.Create(fAnimationTime + Random.Range(-1.0f, 1.0f), EEasing.EASE_NONE, EndRoulette, null, f =>
        {
            rouletteImage.localEulerAngles += new Vector3(0, 0, fAnimationSpeed * Time.deltaTime);
        });
    }

    private Color GetRouletteColor()
    {
        float _angle = rouletteImage.localEulerAngles.z % 22.5f;
        return _angle <= 11.25 ? Color.black : Color.red;
    }
    
    private void EndRoulette()
    {
        if (GetRouletteColor() == scoreIn)
        {
            onWin?.Invoke();
            Player.Instance.AddScore(fScoreIn * 2.0f);
        }
        else
            onLose?.Invoke();

        fScoreIn = 0.0f;
        
        coinsImage.gameObject.SetActive(false);
        GetComponentInParent<GameWindows>().CloseButton.interactable = true;
        redButton.interactable = true;
        blackButton.interactable = true;
    }
}
