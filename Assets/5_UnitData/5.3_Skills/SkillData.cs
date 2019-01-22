using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{
    public string skillName;
    public Texture2D skillIcon;
    public SkillType skillType;
    public DamageType damageType;

    [Space(10)]
    public TargetType targetType;
    public AnimationType animationType;
    public float animationTime;

    [Space(10)]
    public int power;
    public int cost;
    public bool isPassive;
    public bool isBuiltIn;
    public bool isUnique;
    public Animation animation;
}