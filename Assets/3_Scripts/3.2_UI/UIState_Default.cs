using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIState_Default : UIState {

    public UIState_Default(CombatManager CM, UI_Combat UI) : base (CM, UI)
    {
    }

    public override void OnUnitSelected(object sender, EventArgs e, float time, int dmg, DamageType dType)
    {
        if (combatManager.CurrentUnit != null)
            combatManager.CurrentUnit.OnReset();

        BaseUnit character = sender as BaseUnit;
        character.OnTurn();
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        Debug.Log("should enter");

        combatManager.CurrentUnit = combatManager.NextUnitsTurn();
        OnThisUnitsTurn(combatManager.CurrentUnit);

        if (combatManager.CurrentUnit is EnemyUnit)
            UI_Combat.instance.CurrentUIState = new UIState_EnemyTurn(combatManager, UI_Combat.instance);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    void Start () {
    }
	
	void Update () {
		
	}
}
