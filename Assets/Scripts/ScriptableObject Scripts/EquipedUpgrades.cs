using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade List", menuName = "Create Upgrade List")]
public class EquipedUpgrades : ScriptableObject
{
    public List<Upgrade> upgradeList;
}
