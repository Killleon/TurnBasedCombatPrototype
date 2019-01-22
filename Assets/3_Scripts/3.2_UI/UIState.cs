using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class UIState {

    protected CombatManager combatManager;
    protected UI_Combat uiCombat;

    protected UIState(CombatManager CM, UI_Combat UI)
    {
        combatManager = CM;
        uiCombat = UI;
    }

    public virtual void OnThisUnitsTurn(BaseUnit unit)
    {
        if (unit.CheckIsKnockedDown())
            return;

        unit.OnTurn();
    }

    public virtual void OnUnitSelected(object sender, EventArgs e, float time, int dmg, DamageType dType)
    {
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
        foreach (BaseUnit unit in combatManager.unitsList)
        {
            // TODO: delete this, its just for testing knock down
            if (unit.CheckIsKnockedDown())
                return;

            unit.OnReset();
        }
        foreach (CharacterUnit cu in combatManager.charactersList)
        {
            cu.EventOnSelected -= OnUnitSelected;
        }
        foreach (EnemyUnit eu in combatManager.enemiesList)
        {
            eu.OnReset();
            eu.EventOnSelected -= OnUnitSelected;
            eu.EventOnSelected -= SingleTargetSelection;
        }
    }

    IEnumerator DelayForOperations()
    {
        yield return new WaitUntil(()=> combatManager.CurrentUnit is CharacterUnit);

        foreach (Transform t in uiCombat.transform)
            t.GetComponent<Button>().enabled = true;
    }

    public void SingleTargetSelection(object target, EventArgs e, float animationTime, int dmg, DamageType dType)
    {
        if(combatManager.latestUI != null)
            combatManager.latestUI.SetActive(false);

        foreach (Transform t in uiCombat.transform)
            t.GetComponent<Button>().enabled = false;

        new Task(DelayForOperations());

        BaseUnit theTarget = target as BaseUnit;

        foreach(BaseUnit bu in combatManager.unitsList)
            bu.EventOnSelected -= SingleTargetSelection;

        //Debug.Log(combatManager.CurrentUnit + " moving");
        Transform currentPos = combatManager.CurrentUnit.transform;
        Vector3 originalPos = currentPos.position;

        Sequence attackSequence = DOTween.Sequence();
        Tween strike = currentPos.DOMove(new Vector3(theTarget.transform.position.x - 0.5f, theTarget.transform.position.y, theTarget.transform.position.z), 0.3f).SetEase(Ease.OutCubic);
        Tween fallback = currentPos.DOMove(new Vector3(originalPos.x, originalPos.y, originalPos.z), 0.3f).SetEase(Ease.OutCubic);

        attackSequence
            .Append(strike)
            .AppendCallback(() => theTarget.OnHit(dmg, combatManager.CurrentUnit, dType))
            .AppendInterval(animationTime)
            .Append(fallback);

        if (combatManager.turnList.Count > 0)
            combatManager.turnList.RemoveAt(0);

        Debug.Log(combatManager.GetTurnListCount() + " Turns Left.");
        UI_Combat.instance.CurrentUIState = new UIState_Default(combatManager, UI_Combat.instance);
    }

}
