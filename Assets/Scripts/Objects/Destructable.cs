using System;
using UnityEngine;

public class Destructable :MonoBehaviour
{
    [SerializeField] private GameObject _destroyObject;
    //TODO: Кусочки должны самоуничтожаться.
    //Instatiate из пула
    public void Action()
    {
        Instantiate(_destroyObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}