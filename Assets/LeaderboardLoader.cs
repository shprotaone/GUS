using GUS.Core.Data;
using GUS.Core.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GUS.Core.SaveSystem
{
    public class LeaderboardLoader : MonoBehaviour
    {
        [SerializeField] private Transform _tableParent;
        [SerializeField] private Transform _content;
        [SerializeField] private JsonToFirebase _fromFireBase;

        [SerializeField] private List<Slot> _slots = new List<Slot>();
        [SerializeField] private Button _loadButton;

        private void Start()
        {
            _loadButton.onClick.AddListener(Activate);
        }

        private void OnEnable()
        {
            StartCoroutine(LoadRoutine());
        }
        private void Activate()
        {
            //_slots = _content.GetComponentsInChildren<Slot>().ToList();
            _tableParent.gameObject.SetActive(true);
        }

        private IEnumerator LoadRoutine()
        {
            yield return new WaitForSeconds(1);
            yield return _fromFireBase.Load();
            List<PlayerData> list = _fromFireBase.LoadedData.OrderByDescending(dist => dist.commonDistance).ToList();
            SetSlots(list);
            yield return null;
        }

        public void SetSlots(List<PlayerData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == null) return;

                _slots[i].gameObject.SetActive(true);
                _slots[i].Set(data[i].playerName, data[i].commonDistance.ToString());
            }
        }
    }
}

