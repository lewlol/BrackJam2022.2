using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade")]
public class Upgrades : ScriptableObject
{
    public float upgradeInt;
    public string upgradeName;
    public float cost;
    public float value;
}
