using UnityEngine;
using System.Collections;
using UnityEditor;

public class SkillListCreator
{
    [MenuItem("Assets/Create/Skill List")]
    public static SkillList Create()
    {
        SkillList asset = ScriptableObject.CreateInstance<SkillList>();

        AssetDatabase.CreateAsset(asset, "Assets/SkillList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}