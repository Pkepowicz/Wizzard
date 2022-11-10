using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using TMPro;
using static SkillTree;
using UnityEngine.UI;
public class Skill : MonoBehaviour
{
    public int id;
    public TMP_Text TitleText;
    public TMP_Text CostText;

    public int[] ConnectedSkills;

    public void UpdateUI()
    {
        TitleText.text = $"{skillTree.SkillLevels[id]}/{skillTree.SkillCaps[id]}\n{skillTree.SkillNames[id]}";
        CostText.text = $"Cost:{skillTree.SkillPoint}/10 SP";

        GetComponent<Image>().color = skillTree.SkillLevels[id] >= skillTree.SkillCaps[id] ? Color.yellow
            : skillTree.SkillPoint >= 1 ? Color.green : Color.white;
        foreach (var connectedSkill in ConnectedSkills)
        {
            skillTree.SkillList[connectedSkill].gameObject.SetActive(skillTree.SkillLevels[id]>0);
            skillTree.ConnectorList[connectedSkill].SetActive(skillTree.SkillLevels[id]>0);
        }
    }

    public void Buy()
    {
        if (skillTree.SkillPoint < 1 || skillTree.SkillLevels[id] >= skillTree.SkillCaps[id]) return;
        skillTree.SkillPoint -= 10;
        skillTree.SkillLevels[id]++;
        skillTree.UpdateAllSkillUI();
    }
}
