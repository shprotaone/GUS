using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _buy;
    [SerializeField] private Image[] _progressImages;
    [SerializeField] private BuildEnum _buildEnum;

    private BuildsSystem _buildsSystem;

    public void Init(BuildsSystem buildSystem)
    {
        _buildsSystem = buildSystem;
        
        _buy.onClick.AddListener(Buy);
    }

    private void Buy()
    {
        _buildsSystem.Buy(_buildEnum);
    }

    private void RefreshProgress()
    {

    }

    public void LoadProgress()
    {

    }

    public void SetCost(int cost)
    {
        _cost.text= cost.ToString();
    }
}
