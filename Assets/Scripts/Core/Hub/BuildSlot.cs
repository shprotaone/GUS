using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _buy;
    [SerializeField] private Image[] _progressImages;

    private int _currentCost;
    private BuildsSystem _buildsSystem;
    public BuildNameEnum BuildName { get; private set; }
    public BuildStateEnum BuildState { get; private set; }

    public void Init(BuildsSystem buildSystem, BuildData buildData)
    {
        _buildsSystem = buildSystem;
        BuildName = buildData.nameEnum;
        BuildState = buildData.state;
        RefreshProgress(BuildState);

        _buy.onClick.AddListener(Buy);
    }

    private void Buy()
    {
        _buildsSystem.Buy(BuildName, _currentCost);
    }

    public void RefreshProgress(BuildStateEnum buildState)
    {
        if (buildState == BuildStateEnum.None) return;

        for (int i = 0; i < (int)buildState; i++)
        {
            _progressImages[i].color = Color.blue;
        }

        if (buildState == BuildStateEnum.FinishState) _buy.interactable = false;
    }

    public void SetCost(int cost)
    {
        _cost.text= cost.ToString();
        _currentCost = cost;
    }
}