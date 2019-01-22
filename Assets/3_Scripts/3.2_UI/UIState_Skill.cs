using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIState_Skill : UIState {

    public UIState_Skill(CombatManager CM, UI_Combat UI) : base (CM, UI)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        PrepareSkillList();
        ShowSkillList();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        uiCombat.combatSkillList.SetActive(false);
    }

    private void ShowSkillList()
    {
        uiCombat.combatSkillList.SetActive(true);
        combatManager.latestUI = uiCombat.combatSkillList;
    }

    void DestroySkillList()
    {
        foreach (Transform skillToDelete in uiCombat.skillList.transform)
        {
            uiCombat.AddForDelete(skillToDelete.gameObject);
        }
    }

    void PrepareSkillList()
    {
        DestroySkillList();
        foreach (SkillData skill in combatManager.CurrentUnit.mySkillList)
        {
            GameObject _skill = GameObject.Instantiate(uiCombat.skillPrefab) as GameObject;
            _skill.transform.SetParent(uiCombat.skillList.transform);
            _skill.name = skill.skillName;
            _skill.GetComponentInChildren<TextMeshProUGUI>().text = _skill.name;
            _skill.GetComponent<Button>().onClick.AddListener( delegate{ DetermineSkillEvent(skill.targetType, skill.animationType, skill.animationTime, skill.power, skill.damageType);} );
        }
    }

    void DetermineSkillEvent(TargetType tt, AnimationType at, float time, int dmg, DamageType dType)
    {
        foreach (EnemyUnit eu in combatManager.enemiesList)
        {
            eu.DisplayAsSelectable();
            eu.GetDamageData(time, dmg, dType);
            eu.EventOnSelected -= SingleTargetSelection;
            eu.EventOnSelected += SingleTargetSelection;
        }
        foreach (CharacterUnit cu in combatManager.charactersList)
        {
            // Can team kill?
        }
        //Debug.Log(tt + " " + at);
    }
}
