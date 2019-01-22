using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillList", menuName = "Units/SkillList", order = 5)]
public class SkillList : ScriptableObject
{
    public List<SkillData> availableSkills = new List<SkillData>();
}