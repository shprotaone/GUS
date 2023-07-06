using TMPro;
using UnityEngine;

namespace GUS.Core.UI
{
    public class CoinView : MonoBehaviour, ICoinView
    {
        [SerializeField] private TMP_Text _coinText;
        public void Activate(bool flag)
        {
            _coinText.transform.parent.gameObject.SetActive(flag);
        }
        
        public void RefreshCoinsCount(int count)
        {
            _coinText.text = count.ToString();
        }

        public void RefreshCoinWithAnim(int count, int prevValue)
        {
            
        }
    }
}

