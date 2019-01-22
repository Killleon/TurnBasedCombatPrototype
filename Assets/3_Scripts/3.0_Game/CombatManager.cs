using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatManager : MonoBehaviour {

    public GameObject unitsContainer;
    internal List<BaseUnit> unitsList { get { return unitsContainer.transform.GetComponentsInChildren<BaseUnit>().ToList(); } }
    internal List<CharacterUnit> charactersList { get { return unitsList.OfType<CharacterUnit>().ToList(); } }
    internal List<EnemyUnit> enemiesList { get { return unitsList.OfType<EnemyUnit>().ToList(); } }

    public BaseUnit currentTurnUnit;

    private BaseUnit currentUnit;
    internal BaseUnit CurrentUnit {
        get
        {
            if (currentUnit != null)
                return currentUnit;
            else
            {
                Debug.Log("No unit being selected");
                return null;
            }
        }
        set
        {
            if (value is BaseUnit)
                currentUnit = value;
            else
                return;
        }
    }

    private int round;
    public int readinessThreshold;

    public List<KeyValuePair<BaseUnit, int>> thresholdList = new List<KeyValuePair<BaseUnit, int>>();
    public List<BaseUnit> turnList = new List<BaseUnit>();
    private List<BaseUnit> roundList = new List<BaseUnit>();

    internal GameObject latestUI;

    private void Awake()
    {

    }

    void Start() {
        
    }

    void Update() {
        currentTurnUnit = CurrentUnit;
        DeathCleanup();


    }

    public BaseUnit NextUnitsTurn()
    {
        if (turnList.Count <= 0)
            CreateRoundList();

        thresholdList = CreateThresholdMap().ToList();
        UnitTurnCalculate(thresholdList);

        if(turnList.Count <= 0)
        {
            EndRound();
            return null;
        }

        foreach (BaseUnit bu in turnList)
        {
            //Debug.Log(bu);
        }

        CurrentUnit = turnList[0];
        return CurrentUnit;
    }

    public Dictionary<BaseUnit, int> CreateThresholdMap()
    {
        List<BaseUnit> characterSPDList = unitsList.OrderByDescending(c => c.SPD).ToList();
        Dictionary<BaseUnit, int> turnDict = new Dictionary<BaseUnit, int>();
        foreach (BaseUnit bu in characterSPDList)
        {
            if(bu.SPD > 0)
                turnDict.Add(bu, bu.SPD);
        }
        return turnDict;
    }

    /// <summary>
    /// A "RoundList" is List for BaseUnits that are yet to be added to turnList. When this list is empty, that is considered a round.
    /// So check this list to be empty to determine when to end a "Round".
    /// </summary>
    /// <returns></returns>
    public void CreateRoundList()
    {
        if(roundList.Count > 0)
        {
            Debug.Log("Something's not right, the roundList isnt even empty yet why are you creating a new one???!!!");
            return;
        }
        foreach (BaseUnit unit in unitsList)
        {
            if(unit.SPD > 0)
                roundList.Add(unit);
        }
    }

    public void UnitTurnCalculate(List<KeyValuePair<BaseUnit, int>> _tresholdList)
    {
        while (roundList.Count > 0)
        {
            for (int i = 0; i < _tresholdList.Count; i++)
            {
                int originalValue = _tresholdList[i].Value;
                int valueToAdd = originalValue += _tresholdList[i].Key.SPD;
                _tresholdList[i] = new KeyValuePair<BaseUnit, int>(_tresholdList[i].Key, valueToAdd);

                if (_tresholdList[i].Value >= 100)
                {
                    turnList.Add(_tresholdList[i].Key);
                    if(roundList.Contains(_tresholdList[i].Key))
                        roundList.Remove(_tresholdList[i].Key);
                    //Debug.Log("Character: " + thresholdList[i].Key.name + " Is over Threshold with " + thresholdList[i].Value + " Readiness.");
                    _tresholdList[i] = new KeyValuePair<BaseUnit, int>(_tresholdList[i].Key, valueToAdd - readinessThreshold);
                }
            }
        }
    }

    public void EndRound()
    {
        round++;
        //Debug.Log("End of Round " + round);
        CreateRoundList();
    }

    private void DeathCleanup()
    {
        foreach (BaseUnit unit in turnList.ToList<BaseUnit>())
        {
            if (unit == null)
            {
                for (int i = 0; i < thresholdList.Count; ++i)
                {
                    if (thresholdList[i].Key == unit)
                        thresholdList.Remove(thresholdList[i]);
                }
                roundList.Remove(unit);
                turnList.Remove(unit);
            }
        }
    }

    public void MoveUnitTurnToLast(BaseUnit bu)
    {
        List<BaseUnit> result = charactersList.Except(turnList).ToList();
        if (result.Count == charactersList.Count)
        {
            bu.isKnockedDown = false;
            return;
        }

        if (turnList.Contains(bu))
        {
            turnList.RemoveAll(unit => unit == bu);
            turnList.Insert(turnList.Count, bu);
        }
    }

    public int GetTurnListCount()
    {
        return turnList.Count;
    }
}
