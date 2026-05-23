using System;
using UnityEngine;
using UnityEngine.UI;

public enum EWindowsCloseType
{
    CLOSED,
    MINIMIZED,
    MAXIMIZED
}

public class GameWindows : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform closeTransform;
    [SerializeField] private Button minimizeButton;
    [SerializeField] private RectTransform minimizeTransform;
    [SerializeField] private float fTimeFromClosed = 2.0f;
    [SerializeField] private float fTimeFromMinimized = 0.5f;
    [SerializeField] private float fClosingTime = 0.5f;
    [SerializeField] private EEasing easing = EEasing.EASE_NONE;
    private EWindowsCloseType windowsCloseType = EWindowsCloseType.CLOSED;
    private RectTransform rectTransform;
    
    public EWindowsCloseType WindowsCloseType => windowsCloseType;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => Close(EWindowsCloseType.CLOSED, closeTransform.anchoredPosition));
        minimizeButton.onClick.AddListener(() => Close(EWindowsCloseType.MINIMIZED, minimizeTransform.anchoredPosition));
        transform.position =  closeTransform.position;
        transform.localScale = Vector3.zero;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        float _time = windowsCloseType == EWindowsCloseType.CLOSED ? fTimeFromClosed : fTimeFromMinimized;
        rectTransform.DOMoveLocal(Vector3.zero, _time, easing);
        transform.DOScale(Vector3.one, _time, easing);
    }

    public void Close(EWindowsCloseType _windowsCloseType, Vector2 _position)
    {
        rectTransform.DOMoveLocal(_position, fClosingTime, easing);
        transform.DOScale(Vector3.zero, fClosingTime, easing);
        windowsCloseType = _windowsCloseType;
    }
}
