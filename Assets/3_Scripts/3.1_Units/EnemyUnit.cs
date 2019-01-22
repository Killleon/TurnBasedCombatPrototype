using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
    //TODO: Create enemy classes (ie. skeleton, elementals, bandits, etc...)

    // List of actions available to AI
    // Current Action to play
    // Previous Actions???
    // Potential Targets
    // Chosen Target

    public EnemySkillList eSkillList;

    private void Awake()
    {
        InitClass();
    }

    private void Start()
    {

    }

    public virtual void Play(CombatManager CM, UI_Combat UI)
    {

    }
}
