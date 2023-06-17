using UnityEngine;

namespace GUS.Core.Hub.BuildShop
{
    public class Build : MonoBehaviour
    {
        [SerializeField] private BuildContainer _container;
        [SerializeField] private GameObject[] _parts;

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
        }

        public void RefreshData()
        {
            UpdateLayers();
        }

        private void UpdateLayers()
        {
            if (_buildData.state == 0) return;

            for (int i = 0; i < _buildData.state; i++)
            {
                _parts[i].SetActive(true);
                if (i > 0) _parts[i - 1].SetActive(false);
            }
        }
    }

}
