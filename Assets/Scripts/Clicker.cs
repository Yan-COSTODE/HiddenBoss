using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    [SerializeField] private float fBasePointPerClick = 1.0f;
    private float fPointPerClick;

    private void Awake()
    {
        clickButton.onClick.AddListener(AddPoint);
        fPointPerClick = fBasePointPerClick;
    }

    private void AddPoint()
    {
        Player.Instance.AddScore(fPointPerClick);
    }
}
