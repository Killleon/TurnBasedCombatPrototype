using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CharacterUnit : BaseUnit {

    private List<WeaponList> AvailableWeapons { get { return characterClass.availableWeaponLists; } }
    
    [SerializeField]
    private WeaponData MyWeapon;
    public WeaponData myWeapon {
        get
        {
            if (MyWeapon.weaponType == null)
            {
                WeaponList newWeaponList = AvailableWeapons[UnityEngine.Random.Range(0, AvailableWeapons.Count)];
                WeaponData newWeapon = newWeaponList.availableWeaponData[UnityEngine.Random.Range(0, newWeaponList.availableWeaponData.Count)];
                MyWeapon = newWeapon;
                return MyWeapon;
            }
            else
            {
                return MyWeapon;
            }
        }
        set
        {
            WeaponTypeData newWeaponType = (WeaponTypeData)value.weaponType;
            if (MyWeapon.weaponType != newWeaponType)
            {
                Debug.Log("Weapon Types dont match");
                return;
            }
            else
            {
                Debug.Log("Success!!");
                MyWeapon = value;
            }
        }
    }

    internal List<SkillData> weaponSkillList {
        get
        {
            // TODO: Remember to separate weaponSkillList from availableSkills. The former is the skill you ACTUALLY have, the latter is a list of potential skills the weapon can hold.
            return myWeapon.weaponSkills.availableSkills;
        }
    }

    private void Awake()
    {
        if (characterClass == null) throw new Exception("Empty CharacterClass");
        InitClass();
    }

    void Start () {
        WeaponList newWeaponList = AvailableWeapons[UnityEngine.Random.Range(0, AvailableWeapons.Count)];
        MyWeapon = newWeaponList.availableWeaponData[UnityEngine.Random.Range(0, newWeaponList.availableWeaponData.Count)];

        mySkillList = weaponSkillList;
    }

    public void EquipSkill(SkillData newSkill)
    {
        bool canEquipSkill = weaponSkillList.Contains(newSkill);
        bool sufficientSkillSlots = mySkillList.Count < maxSkillCount;

        if (!canEquipSkill)
        {
            Debug.Log("Skill not for you.");
        }
        else if (!sufficientSkillSlots)
        {
            Debug.Log("Skill slots is full.");
        }
        else
        {
            // Equip it...
            Debug.Log("Equipped Skill!");
        }
    }

}
