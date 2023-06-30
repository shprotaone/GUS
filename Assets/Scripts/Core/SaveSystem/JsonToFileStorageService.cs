using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace GUS.Core.SaveSystem
{
    public class JsonToFileStorageService : IStorageService
    {
        //private bool _isInProgressNow;
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            string json = JsonConvert.SerializeObject(data);

            
            using (var fileStream = new StreamWriter(path))
            {
                fileStream.Write(json);
            }

            callback?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            try
            {
                using (var filestream = new StreamReader(path))
                {
                    var json = filestream.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<T>(json);
                    callback.Invoke(data);
                }
            }
            catch(Exception ex)
            {
                Delete(key);
                return;
            }                   
        }    

        public void Delete(string key)
        {
            File.Delete(BuildPath(key));
            Debug.LogWarning("Сохранение удалено");
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}

