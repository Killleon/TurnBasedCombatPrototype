using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SkillListEditor : EditorWindow
{

    public SkillList SkillList;
    private int viewIndex = 1;

    [MenuItem("Window/Skill List Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(SkillListEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            SkillList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(SkillList)) as SkillList;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Skill List Editor", EditorStyles.boldLabel);
        if (SkillList != null)
        {
            if (GUILayout.Button("Show Skill List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = SkillList;
            }
        }
        if (GUILayout.Button("Open Skill List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Skill List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = SkillList;
        }
        GUILayout.EndHorizontal();

        if (SkillList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Skill List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Skill List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (SkillList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < SkillList.availableSkills.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Skill", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Skill", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (SkillList.availableSkills == null)
                Debug.Log("wtf");
            if (SkillList.availableSkills.Count > 0)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Skill", viewIndex, GUILayout.ExpandWidth(false)), 1, SkillList.availableSkills.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.availableSkillTypes.Count);
                EditorGUILayout.LabelField("of   " + SkillList.availableSkills.Count.ToString() + "  skills", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.Space(20);
                SkillList.availableSkills[viewIndex - 1].skillIcon = EditorGUILayout.ObjectField(GUIContent.none, SkillList.availableSkills[viewIndex - 1].skillIcon, typeof(Texture2D), false, GUILayout.Width(65)) as Texture2D;

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].skillName = EditorGUILayout.TextField("Skill Name", SkillList.availableSkills[viewIndex - 1].skillName as string, GUILayout.Width(400));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].skillType = (SkillType)EditorGUILayout.EnumPopup("Skill Type", SkillList.availableSkills[viewIndex - 1].skillType, GUILayout.Width(300));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].damageType = (DamageType)EditorGUILayout.EnumPopup("Damage Type", SkillList.availableSkills[viewIndex - 1].damageType, GUILayout.Width(300));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].power = EditorGUILayout.IntField("Power", SkillList.availableSkills[viewIndex - 1].power, GUILayout.Width(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].cost = EditorGUILayout.IntField("Cost", SkillList.availableSkills[viewIndex - 1].cost, GUILayout.Width(200));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].isPassive = (bool)EditorGUILayout.Toggle("Passive Skill", SkillList.availableSkills[viewIndex - 1].isPassive);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].isBuiltIn = (bool)EditorGUILayout.Toggle("Built-In", SkillList.availableSkills[viewIndex - 1].isBuiltIn);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                SkillList.availableSkills[viewIndex - 1].isUnique = (bool)EditorGUILayout.Toggle("Unique", SkillList.availableSkills[viewIndex - 1].isUnique);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
            }
            else
            {
                GUILayout.Label("Skill List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(SkillList);
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        SkillList = SkillListCreator.Create();
        if (SkillList)
        {
            SkillList.availableSkills = new List<SkillData>();
            string relPath = AssetDatabase.GetAssetPath(SkillList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Skill List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            SkillList = AssetDatabase.LoadAssetAtPath(relPath, typeof(SkillList)) as SkillList;
            if (SkillList.availableSkills == null)
                SkillList.availableSkills = new List<SkillData>();
            if (SkillList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        SkillData newSkill = new SkillData();
        newSkill.skillName = "New Skill";
        SkillList.availableSkills.Add(newSkill);
        viewIndex = SkillList.availableSkills.Count;
    }

    void DeleteItem(int index)
    {
        SkillList.availableSkills.RemoveAt(index);
    }
}