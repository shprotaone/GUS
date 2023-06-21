using GUS.Core.Data;
using TMPro;
using UnityEngine;

public class HonkCoinView : MonoBehaviour,IHonkCoinView
{
    [SerializeField] private TMP_Text _text;

    public void Activate(bool flag)
    {
        _text.transform.parent.gameObject.SetActive(flag);
    }

    public void Refresh(int value)
    {
        _text.text = value.ToString();
    }
}
