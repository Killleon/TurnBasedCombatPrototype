using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour {

}

public class SkillEventArgs : EventArgs
{
    float t;
    public SkillEventArgs(int t)
    {
        this.t = t;
    }
    public float T { get { return t; } }
}

public static class ListExtenstions
{
    public static void AddMany<T>(this List<T> list, params T[] elements)
    {
        list.AddRange(elements);
    }
}

public enum ClassType
{
    Fighter,
    Mage,
    Rogue,
    Ranger
}

public enum WeaponType
{
    Sword,
    Hammer,
    Polearm,
    Dagger,
    Bow,
    CrossBow,
    MagicOrb
}

public enum SkillType
{
    Ability,
    Magic
}

public enum TargetType
{
    Single,
    AOE
}

public enum AnimationType
{
    Melee,
    SpellCast
}

public enum DamageType
{
    None,
    Slash,
    Blunt,
    Pierce,
    Fire,
    Water,
    Air,
    Shock,
    Light,
    Dark,
    True
}