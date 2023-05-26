using Firebase.Database;
using GUS.Core.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace GUS.Core.SaveSystem
{
    public class JsonToFirebase : MonoBehaviour
    {
        private IStorageService _storageService;
        private DatabaseReference _databaseReference;
        private List<PlayerData> _datas;

        private PlayerData _data;
        public List<PlayerData> Datas => _datas;
        public PlayerData Data => _data;

        private void Start()
        {
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            _datas = new List<PlayerData>();
        }

        public void Save(PlayerData data)
        {
            string result = JsonConvert.SerializeObject(data);
            _databaseReference.Child("users").Child(data.playerName).SetValueAsync(result);
            _databaseReference.Child("users").Child(data.playerName).Child("coins").SetValueAsync(data.coins);
            _databaseReference.Child("users").Child(data.playerName).Child("distance").SetValueAsync(data.commonDistance);
        }

        public IEnumerator Load()
        {
            _datas.Clear();
            var users = _databaseReference.Child("users").GetValueAsync();

            yield return new WaitUntil(predicate: () => users.IsCompleted);

            if (users.Exception != null)
            {
                Debug.LogError(users.Exception);
            }
            else if (users.Result == null)
            {
                Debug.Log("Пользователя нет");
            }
            else
            {
                DataSnapshot snapshot = users.Result;
                foreach (var childs in snapshot.Children.Reverse())
                {
                    PlayerData data = new PlayerData();
                    data = JsonConvert.DeserializeObject<PlayerData>((string)childs.Value);
                    _datas.Add(data);  //TODO: развязать
                }
            }
        }
    }
}

