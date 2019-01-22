using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIState_Attack : UIState{

    public UIState_Attack(CombatManager CM, UI_Combat UI) : base (CM, UI)
    {
    }

    public override void OnStateEnter() {
        base.OnStateEnter();

        foreach (EnemyUnit eu in combatManager.enemiesList)
        {
            CharacterUnit cu = combatManager.CurrentUnit as CharacterUnit;
            eu.DisplayAsSelectable();
            eu.EventOnSelected += SingleTargetSelection;
            eu.GetDamageData(1f, cu.ATK, cu.myWeapon.weaponType.damageType);

        }
        foreach (CharacterUnit cu in combatManager.charactersList)
        {
            // Can team kill?
        }
    }

    public override void OnStateExit() {
        base.OnStateExit();
    }

}
