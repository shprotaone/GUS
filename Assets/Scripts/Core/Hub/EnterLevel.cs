using GUS.Player;
using UnityEngine;


public class EnterLevel : MonoBehaviour
{
    [SerializeField] private SceneHandler _sceneHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerActor>(out PlayerActor actor))
        {
            _sceneHandler.LoadRunScene();
        }
    }
}
