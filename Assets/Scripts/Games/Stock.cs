using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stock : MonoBehaviour
{
    public Action onCashIn;
    public Action onCashOutPos;
    public Action onCashOutNeg;
    
    [SerializeField] private GameWindows gameWindows;
    [SerializeField] private Button holdButton;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private RectTransform visualStock;
    [SerializeField] private Image visualStockImage;
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;
    [SerializeField] private float fCooldown = 1.0f;
    [SerializeField] private float fStockSpeed = 1.0f;
    [SerializeField] private TMP_Text stockText;
    private float fCurrentStock = 0.0f;
    private double fScoreIn = 0.0f;

    private void Awake()
    {
        holdButton.onClick.AddListener(ToggleStock);
    }

    private void Start()
    {
        gameWindows.OnClosed += TakeStock;
        TimerManager.Instance.Create(fStockSpeed, EEasing.EASE_NONE, GenerateStockAmount, null, null, true);
    }

    private void OnDestroy()
    {
        onCashIn = null;
        onCashOutNeg = null;
        onCashOutPos = null;
    }

    private void GenerateStockAmount()
    {
        float _min = -14.0f;
        float _max = 4.0f;
        fCurrentStock = Random.Range(_min, _max);
        visualStockImage.color = fCurrentStock < 0 ? negativeColor : positiveColor;
        visualStock.localScale = fCurrentStock < 0 ? new Vector3(1.0f, fCurrentStock / -_min, 1.0f) : new Vector3(1.0f, fCurrentStock / _max, 1.0f);
        stockText.text = $"{(fCurrentStock < 0 ? "-" : "+")}{Mathf.Abs(fCurrentStock):P}";
        stockText.color = visualStockImage.color;
    }
    
    private void ToggleStock()
    {
        if (fScoreIn == 0.0f)
            InvestStock();
        else
            TakeStock();
    }
    
    private void InvestStock()
    {
        if (Player.Instance.PlayerData.score <= 0.0)
            return;
        
        onCashIn?.Invoke();
        buttonText.text = "Cash Out";
        fScoreIn = Player.Instance.PlayerData.score;
        Player.Instance.AddScore(-fScoreIn);
        holdButton.interactable = false;
        TimerManager.Instance.Create(fCooldown, EEasing.EASE_NONE, () => holdButton.interactable = true);
    }

    private void TakeStock()
    {
        if (fScoreIn <= 0.0)
            return;
        
        buttonText.text = "Cash In";

        if (fCurrentStock >= 0)
        {
            onCashOutPos?.Invoke();
            Player.Instance.AddScore(fScoreIn * (1 + fCurrentStock));
        }
        else
        {
            onCashOutNeg?.Invoke();
            Player.Instance.AddScore(fScoreIn / (1 + -fCurrentStock));
        }
        
        fScoreIn = 0.0f;
        holdButton.interactable = false;
        TimerManager.Instance.Create(fCooldown, EEasing.EASE_NONE, () => holdButton.interactable = true);
    }
}
