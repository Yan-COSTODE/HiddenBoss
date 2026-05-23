using UnityEngine;
using UnityEngine.UI;

public class GameWindows : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform closeTransform;
    [SerializeField] private float fOpeningTime = 2.0f;
    [SerializeField] private float fClosingTime = 0.5f;
    [SerializeField] private EEasing easing = EEasing.EASE_NONE;
    private RectTransform rectTransform;
    

    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
        transform.position = closeTransform.position;
        transform.localScale = Vector3.zero;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        float _time = fOpeningTime;
        rectTransform.DOMoveLocal(Vector3.zero, _time, easing);
        transform.DOScale(Vector3.one, _time, easing);
    }

    public void Close()
    {
        rectTransform.DOMoveLocal(closeTransform.anchoredPosition, fClosingTime, easing);
        transform.DOScale(Vector3.zero, fClosingTime, easing);
    }
}
