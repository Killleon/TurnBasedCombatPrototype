using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassData", menuName = "Units/ClassData", order = 1)]
public class ClassData : ScriptableObject {

    public ClassType classType;

    public int HPBase;
    public int MPBase;
    public int ATKBase;
    public int DEFBase;
    public int MAGBase;
    public int SPDBase;
    public int LCKBase;

    public List<WeaponList> availableWeaponLists = new List<WeaponList>();

}

