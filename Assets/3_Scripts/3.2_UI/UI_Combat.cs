using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Combat : Singleton<UI_Combat>
{
    [SerializeField]
    private CombatManager CM;

    [Space(10)]
    public GameObject combatSkillList;
    public GameObject RaidCommands;
    public GameObject skillList;
    public GameObject skillPrefab;

    private BaseUnit currentUnit { get { return CM.CurrentUnit; } }
    private UIState currentUIState;
    public UIState CurrentUIState
    {
        get { return currentUIState; }
        set
        {
            if (currentUIState != null)
            {
                if (currentUIState.ToString() == value.ToString())
                    return;

                currentUIState.OnStateExit();
            }
                
            currentUIState = value;
            currentUIState.OnStateEnter();
        }
    }

    public void AddForDelete(GameObject toDestroy)
    {
        Destroy(toDestroy);
    }

    public void EnterDefaultState()
    {
        CurrentUIState = new UIState_Default(CM, this);
    }

    public void EnterAttackState()
    {
        CurrentUIState = new UIState_Attack(CM, this);
    }

    public void EnterSkillState()
    {
        CurrentUIState = new UIState_Skill(CM, this);
    }

    public void EnterEnemyTurnState()
    {
        CurrentUIState = new UIState_EnemyTurn(CM, this);
    }

    void Start () {
        DOTween.Init();
        CurrentUIState = new UIState_Default(CM, this);
    }

    void Update()
    {
        List<EnemyUnit> knockedDownList = CM.enemiesList.FindAll(e => e.CheckIsKnockedDown());
        if (knockedDownList.Count == CM.enemiesList.Count)
        {
            CurrentUIState = new UIState_Raid(CM, this);
            return;
        }

        if (Input.GetMouseButtonDown(1) && CM.latestUI != null)
        {
            CM.latestUI.SetActive(false);
            CM.latestUI = null;

            foreach (BaseUnit bu in CM.unitsList)
            {
                bu.EventOnSelected -= CurrentUIState.SingleTargetSelection;
                bu.OnReset();
            }
            //TODO: change appearance color of latest/previous latestUI.
        }

    }

}
