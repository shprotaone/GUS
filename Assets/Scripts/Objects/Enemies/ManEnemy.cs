using DG.Tweening;
using UnityEngine;

public class ManEnemy : MonoBehaviour,IEnemy
{
    [SerializeField] private Animator _animator;
    private ClickerGame _game;
    public void Behaviour(float value)
    {
        
    }

    public void Death()
    {
        transform.DOMove(Vector3.left * 10, 2).OnComplete(() => gameObject.SetActive(false));
    }

    public void Init(ClickerGame clicker)
    {
        gameObject.SetActive(true);
        _game = clicker;
        transform.DOMove(clicker.RunPoint.position, 2);
    }
}
