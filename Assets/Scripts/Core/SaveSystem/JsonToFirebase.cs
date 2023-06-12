using Firebase.Database;
using GUS.Core.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using Firebase.Auth;
using GUS.Core.Data;

namespace GUS.Core.SaveSystem
{
    public class JsonToFirebase : MonoBehaviour
    {
        private IStorageService _storageService;
        private DatabaseReference _databaseReference;
        private FirebaseAuth _firebaseAuth;
        private List<PlayerData> _loadData;

        private PlayerData _data;
        public List<PlayerData> LoadedData => _loadData;

        private void Awake()
        {
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            _firebaseAuth = FirebaseAuth.DefaultInstance;
            _loadData = new List<PlayerData>();
        }

        private void Auth(string email,string password)
        {
            //_auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            //    if (task.IsCanceled)
            //    {
            //        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            //        return;
            //    }
            //    if (task.IsFaulted)
            //    {
            //        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            //        return;
            //    }

                //// Firebase user has been created.
                //Firebase.Auth.AuthResult result = task.Result;
                //Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                //    result.User.DisplayName, result.User.UserId);
        }

        public void Save(PlayerData data)
        {
            string result = JsonConvert.SerializeObject(data);
            _databaseReference.Child("users").Child(data.playerName).SetRawJsonValueAsync(result);
        }

        public void Delete(PlayerData data)
        {
            _databaseReference.Child("users").Child(data.playerName).RemoveValueAsync();
        }

        public IEnumerator Load()
        {
            _loadData.Clear();
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
                foreach (var child in snapshot.Children.Reverse())
                {
                    PlayerData data = new PlayerData();
                    data = JsonConvert.DeserializeObject<PlayerData>(child.GetRawJsonValue());                    
                    _loadData.Add(data);  //TODO: развязать
                }
            }
        }

        private void HandleValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }

            Debug.Log("Sorted");
        }
    }
}

