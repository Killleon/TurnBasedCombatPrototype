using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponType", order = 1)]
public class WeaponTypeData : ScriptableObject
{
    public WeaponType weaponType;
    public DamageType damageType;
    public DamageType damageType2;

    [Space(10)]
    public SkillList availableSkills;

}
