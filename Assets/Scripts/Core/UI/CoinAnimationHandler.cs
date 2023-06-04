using UnityEngine;

public class CoinAnimationHandler : MonoBehaviour
{
    [SerializeField] private Transform _cornsObj;
    [SerializeField] private Transform _enpPos;
    [SerializeField] private CoinAnim[] _coins;

    public void Animate()
    {
        _cornsObj.gameObject.SetActive(true);

        foreach (CoinAnim coinAnim in _coins)
        {
            coinAnim.Movement(_enpPos.position);
        }
    }
}