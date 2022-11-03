using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public int SkillPoint;

    private void Start()
    {
        SkillPoint = 20;
        SkillLevels = new int[6];
        SkillCaps = new[] {1, 5, 5, 2, 10, 10};
        SkillNames = new[] {"one", "two", "three", "four", "five", "six"};
        SkillDescriptions = new[]
        {
             "two", "three", "four", "five", "six","one"
        };


        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach(var skill in SkillList) skill.UpdateUI();
    }
}
