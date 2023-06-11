using GUS.LevelBuild;
using GUS.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace GUS.Core.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private PoolTypeEnum _poolType;
        private List<ObjectInfo> _objectsInfo;
        private Dictionary<PoolObjectType, Pool> pools;

        public List<ObjectInfo> ObjectsInfo => _objectsInfo;
        public PoolObjectStorage Storage { get; private set; }

        public void InitPool(PoolObjectStorage storage)
        {
            Storage = storage;
            FillObjects(storage.parts);
            pools = new Dictionary<PoolObjectType, Pool>();

            GameObject emptyGO = new GameObject();

            foreach (ObjectInfo item in _objectsInfo)
            {
                GameObject container = Instantiate(emptyGO, transform, false);

                container.name = item.ObjectType.ToString();

                pools[item.ObjectType] = new Pool(container.transform);

                for (int i = 0; i < item.startCount; i++)
                {
                    GameObject go = InstantiateObject(item.ObjectType, container.transform);
                    pools[item.ObjectType].Objects.Enqueue(go);
                }
            }

            Destroy(emptyGO);
        }

        private void FillObjects(List<ObjectInfo> objects)
        {
            _objectsInfo = new List<ObjectInfo>();

            foreach (var part in objects)
            {
                part.Init();
                _objectsInfo.Add(part);
            }            
        }

        private GameObject InstantiateObject(PoolObjectType type, Transform parent)
        {
            var go = Instantiate(_objectsInfo.Find(x => x.ObjectType == type).prefab, parent);
            go.gameObject.SetActive(false);
            return go.gameObject;
        }

        public GameObject GetObject(PoolObjectType type)
        {
            var obj = pools[type].Objects.Count > 0 ?
                pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);
            obj.SetActive(true);

            return obj;
        }

        public void DestroyObject(GameObject obj)
        {
            obj.SetActive(false);
            PoolObjectType type = obj.GetComponent<IPoolObject>().Type;
            pools[type].Objects.Enqueue(obj);           
            obj.transform.SetParent(pools[type].Container);
            obj.transform.position = pools[type].Container.transform.position;           
        }
    }

}
