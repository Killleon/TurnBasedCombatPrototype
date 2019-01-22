using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponList", menuName = "Weapons/WeaponList", order = 5)]
public class WeaponList : ScriptableObject
{
    public List<WeaponData> availableWeaponData = new List<WeaponData>();
}