using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAbilityList", menuName = "Enemy/EnemySkillList", order = 5)]
public class EnemySkillList : ScriptableObject
{
    public List<SkillData> availableSkills = new List<SkillData>();
}