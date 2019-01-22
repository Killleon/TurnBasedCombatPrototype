using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIState_Raid : UIState {

    public UIState_Raid(CombatManager CM, UI_Combat UI) : base (CM, UI)
    {

    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        uiCombat.RaidCommands.SetActive(true);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

}
