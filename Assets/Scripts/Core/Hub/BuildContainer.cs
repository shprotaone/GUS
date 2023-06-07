using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildContainer : ScriptableObject
{
    public BuildNameEnum buildName;
    public int[] costs;
}
