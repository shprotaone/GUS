using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private BuildContainer _container;
    [SerializeField] private GameObject[] _parts;
    [SerializeField] private BuildSlotView _view;
    [SerializeField] private int _steps;

    private BuildsSystem _buildSystem;
    private BuildData _buildData;
    public BuildContainer Container => _container;
    public int StepCount => _steps;

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
        _view.RefreshProgress(_buildData.state,StepCount);
        _view.SetCost(_container.costs[(int)_buildData.state]);

    }

    private void UpdateLayers()
    {
        if (_buildData.state == BuildStateEnum.None) return;

        for (int i = 0; i < (int)_buildData.state; i++)
        {           
            _parts[i].SetActive(true);
            if(i > 0) _parts[i - 1].SetActive(false);
            Debug.Log(i);
        }
    }
}
