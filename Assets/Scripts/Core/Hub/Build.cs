using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private BuildContainer _container;
    [SerializeField] private GameObject[] _parts;
    [SerializeField] private BuildSlot _view;

    private BuildsSystem _buildSystem;
    private BuildData _buildData;
    public BuildContainer Container => _container;

    public void Init(BuildsSystem system, BuildData buildData)
    {
        _buildSystem = system;
        _buildData = buildData;

        RefreshData();
        _view.Init(_buildSystem, _buildData);
    }

    public void RefreshData()
    {
        UpdateLayers();
        _view.RefreshProgress(_buildData.state);
        _view.SetCost(_container.costs[(int)_buildData.state]);
    }

    private void UpdateLayers()
    {
        if (_buildData.state == BuildStateEnum.None) return;

        for (int i = 0; i < (int)_buildData.state; i++)
        {
            _parts[i].SetActive(true);
            Debug.Log(i);
        }
    }
}
