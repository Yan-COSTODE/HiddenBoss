using System;
using UnityEngine;
using UnityEngine.UI;

public class GameIcon : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameWindows gameWindows;

    private void Awake()
    {
        button.onClick.AddListener(gameWindows.Open);
    }
}
