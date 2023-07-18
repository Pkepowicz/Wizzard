using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;
    public int[] SkillPointer;

    public List<Skill> SkillList;
    public GameObject SkillHolder;
    
    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    public int SkillPoint;

    private void Start()
    {
        SkillPoint = 20;
        SkillLevels = new int[14];
        SkillCaps = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1, 1, 1};
        SkillNames = new[] {"Fire Magic", "Knocback I", "Knockback II", "Knockback III", "Explosion I", "Explosion II","Explosion III", "Damage I", "Damage II", "Damage III", "Damage IV", "Damage V", "Multishoot I", "Multishoot II"};
        SkillDescriptions = new[]
        {
             "The most destructive magic", "Boost your damage", "Kill everybody around you", "Summon fire elemental", "Be a fire","Your shot explode","The most destructive magic", "Boost your damage", "Kill everybody around you", "Summon fire elemental", "Be a fire",
        };


        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);
        
        
        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] {1, 4, 7};
        SkillList[1].ConnectedSkills = new[] {2};
        SkillList[2].ConnectedSkills = new[] {3};
        SkillList[3].ConnectedSkills = new[] {12};
        SkillList[4].ConnectedSkills = new[] {5};
        SkillList[5].ConnectedSkills = new[] {6};
        SkillList[6].ConnectedSkills = new[] {12};
        SkillList[7].ConnectedSkills = new[] {8};
        SkillList[8].ConnectedSkills = new[] {9};
        SkillList[9].ConnectedSkills = new[] {10};
        SkillList[10].ConnectedSkills = new[] {11};
        SkillList[11].ConnectedSkills = new[] {12};
        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach(var skill in SkillList) skill.UpdateUI();
    }
}
