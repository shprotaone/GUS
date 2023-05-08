using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCount;
    [SerializeField] private Button _retryButton;
    
    public Button RetryButton => _retryButton;

    public void RefreshCoinsCount(int count)
    {
        _coinsCount.text = count.ToString();
    }
}
