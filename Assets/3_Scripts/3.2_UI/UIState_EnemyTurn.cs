using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState_EnemyTurn : UIState {

    public UIState_EnemyTurn(CombatManager CM, UI_Combat UI) : base (CM,UI)
    {
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OnThisUnitsTurn(combatManager.CurrentUnit);
        new Task(DelayForOperations());
    }

    IEnumerator DelayForOperations()
    {
        yield return new WaitForSeconds(1f);
        EnemyUnit enemy = combatManager.CurrentUnit.GetComponent<EnemyUnit>();

        if (enemy != null && !enemy.CheckIsKnockedDown())
            enemy.Play(combatManager, uiCombat);
        else if (enemy != null && enemy.CheckIsKnockedDown())
            enemy.KnockDown(combatManager, uiCombat);

        UI_Combat.instance.CurrentUIState = new UIState_Default(combatManager, UI_Combat.instance);

    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
