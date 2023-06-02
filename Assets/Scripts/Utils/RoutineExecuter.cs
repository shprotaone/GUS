using System.Collections;
using UnityEngine;

public class RoutineExecuter : MonoBehaviour
{
    public void Execute(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void Stop(IEnumerator routine)
    {
        StopCoroutine(routine);
    }

    public void AllStop()
    {
        StopAllCoroutines();
    }
}
