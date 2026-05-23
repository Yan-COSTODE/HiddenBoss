using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private GameObject handler;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text volumeText;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float _volume)
    {
        AudioListener.volume = _volume / 100.0f;
        volumeText.text = $"{_volume:N0}%";
    }

    public void ToggleHandler()
    {
        handler.SetActive(!handler.activeSelf);
    }
}
