using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string weaponName;
    public WeaponTypeData weaponType;
    [Space(10)]

    public int MPBonus;
    public int HPBonus;
    public int ATKBonus;
    public int MAGBonus;
    public int DEFBonus;
    public int SPDBonus;
    public int LCKBonus;
    public int CST;
    [Space(10)]

    public SkillList weaponSkills;

}
