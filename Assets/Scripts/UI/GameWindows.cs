using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWindows : MonoBehaviour
{
    public Action OnOpened;
    public Action OnClosed;
    
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform closeTransform;
    [SerializeField] private float fOpeningTime = 2.0f;
    [SerializeField] private float fClosingTime = 0.5f;
    [SerializeField] private EEasing easing = EEasing.EASE_NONE;
    private RectTransform rectTransform;
    
    public Button CloseButton => closeButton;
    
    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
        transform.position = closeTransform.position;
        transform.localScale = Vector3.zero;
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        OnOpened = null;
        OnClosed = null;
    }

    public void Open()
    {
        OnOpened?.Invoke();
        ControlManager.Instance.SetClickBlocker(true);
        rectTransform.DOMoveLocal(Vector3.zero, fOpeningTime, easing);
        transform.DOScale(Vector3.one, fOpeningTime, easing);
        TimerManager.Instance.Create(fOpeningTime, EEasing.EASE_NONE, () => ControlManager.Instance.SetClickBlocker(false));
    }

    public void Close()
    {
        OnClosed?.Invoke();
        ControlManager.Instance.SetClickBlocker(true);
        rectTransform.DOMoveLocal(closeTransform.anchoredPosition, fClosingTime, easing);
        transform.DOScale(Vector3.zero, fClosingTime, easing);
        TimerManager.Instance.Create(fClosingTime, EEasing.EASE_NONE, () => ControlManager.Instance.SetClickBlocker(false));
    }
}
