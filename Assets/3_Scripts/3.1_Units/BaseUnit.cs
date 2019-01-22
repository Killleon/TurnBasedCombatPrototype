using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour, ISelectable {

    internal ClassType myClassType;
    internal List<SkillData> mySkillList;
    public ClassData characterClass;

    public int maxSkillCount;
    public event Action<BaseUnit, EventArgs, float, int, DamageType> EventOnSelected;

    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public int MAG;
    public int SPD;
    public int LCK;

    public List<DamageType> weakness;
    public List<DamageType> resistances;
    internal bool isKnockedDown;

    private float enemyAnimationTime;
    private int enemyDamageDealt;
    private DamageType enemyDamageType;

    public BaseUnit()
    {

    }

    private void Start()
    {
        
    }

    void Update()
    {
        OnSelectedBySkill();
    }

    public void InitClass()
    {
        myClassType = characterClass.classType;
        mySkillList = new List<SkillData>();
        maxSkillCount = 6;

        HP += characterClass.HPBase;
        MP += characterClass.MPBase;
        ATK += characterClass.ATKBase;
        DEF += characterClass.DEFBase;
        MAG += characterClass.MAGBase;
        SPD += characterClass.SPDBase;
        LCK += characterClass.LCKBase;
    }

    public void GetDamageData(float time, int dmg, DamageType dType)
    {
        GetAnimationTime(time);
        GetDamageDealt(dmg);
        GetDamageType(dType);
    }

    public void GetAnimationTime(float time)
    {
        enemyAnimationTime = time;
    }

    public void GetDamageDealt(int dmg)
    {
        enemyDamageDealt = dmg;
    }

    public void GetDamageType(DamageType dType)
    {
        enemyDamageType = dType;
    }

    public void OnSelectedBySkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit && hit.transform.gameObject.GetComponent<BaseUnit>() != null && hit.transform.gameObject == gameObject)
            {
                if (EventOnSelected != null) EventOnSelected.Invoke(gameObject.GetComponent<BaseUnit>(), new EventArgs(), enemyAnimationTime, enemyDamageDealt, enemyDamageType);
            }
        }
    }

    protected virtual void OnMouseDown()
    {
        
    }

    public virtual void OnTurn()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public virtual void DisplayAsSelectable()
    {
        //GetComponent<SpriteRenderer>().color = new Color(0.85f, 0.85f, 0.65f);
    }

    public virtual void OnReset()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    public virtual void Attack(BaseUnit target)
    {
        transform.Translate(target.transform.position*Time.deltaTime);
        // TODO: Attack behaviour
    }

    public virtual void OnHit(int dmg, BaseUnit attacker, DamageType dType)
    {
        int totalDmg;

        if (weakness.Contains(dType))
        {
            Debug.Log("Weakness hit!");
            totalDmg = dmg * 2;
            isKnockedDown = true;
        }
        else if (resistances.Contains(dType))
        {
            Debug.Log("Resistant!");
            totalDmg = dmg / 2;
        }
        else
        {
            totalDmg = dmg;
        }

        HP -= totalDmg;

        if ( HP <= 0 )
        {
            DeathSequence();
        }
    }

    public virtual bool CheckIsKnockedDown()
    {
        return isKnockedDown;
    }

    public virtual void KnockDown(CombatManager CM, UI_Combat UI)
    {
        CM.MoveUnitTurnToLast(this);
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void DeathSequence()
    {
        // TODO: insert enemy last words
        print(gameObject.name + " Died.");
        Destroy(gameObject);
    }

    public virtual int GetRandomNumber()
    {
        int randomNumber;
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        randomNumber = (int)UnityEngine.Random.Range(1, 100);
        return randomNumber;
    } 
}
