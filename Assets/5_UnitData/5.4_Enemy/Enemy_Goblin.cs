using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Goblin : EnemyUnit
{
    public override void Play(CombatManager CM, UI_Combat UI)
    {
        base.Play(CM, UI);
        int resultingRoll = transform.GetComponent<EnemyUnit>().GetRandomNumber();
        Debug.Log(gameObject.name + " rolled " + resultingRoll);

        if (resultingRoll >= 1 && resultingRoll <= 50)
        {
            Debug.Log("Used: " + eSkillList.availableSkills[0].skillName);
        }
        else
        {
            Debug.Log("Used: " + eSkillList.availableSkills[1].skillName);
        }

        if (CM.turnList.Count > 0)
            CM.turnList.RemoveAt(0);

        Debug.Log(CM.GetTurnListCount() + " Turns Left.");
    }
}
