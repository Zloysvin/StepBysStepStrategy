using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSkills : MonoBehaviour
{
    public Button[] abilitiesList = new Button[3];

    private void Start()
    {
        string[] lines = File.ReadAllLines(@"__TextFiles\ActiveAbilitySave.sav");
        for (int i = 0; i < 3; i++)
        {
            if (lines[i] != "#")
               abilitiesList[i].GetComponentInChildren<Text>().text = lines[i];
        }
    }
    public void ChangeText(Button skill)
    {
        for (int i = 0; i < 3; i++)
        {
            if (abilitiesList[i].tag.Equals("Selected"))
            {
                abilitiesList[i].GetComponentInChildren<Text>().text = skill.GetComponentInChildren<Text>().text;
                abilitiesList[i].tag = "Untagged";
                abilitiesList[i].GetComponent<Image>().color = Color.white;

                string[] lines = File.ReadAllLines(@"__TextFiles\ActiveAbilitySave.sav");
                lines[i] = skill.GetComponentInChildren<Text>().text;
                File.WriteAllLines(Directory.GetCurrentDirectory() + @"\__TextFiles\ActiveAbilitySave.sav", lines);
            }
            else
            {
                abilitiesList[i].tag = "Untagged";
                abilitiesList[i].GetComponent<Image>().color = Color.white;
            }
        }     
        
    }

    public void SelectSkill(Button btn)
    {
        int index = Convert.ToInt32(btn.name[btn.name.Length - 1]) - 49;
        for (int i = 0; i < 3; i++)
        {
            if (i != index)
            {
                abilitiesList[i].tag = "Untagged";
                abilitiesList[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                abilitiesList[i].tag = "Selected";
                abilitiesList[i].GetComponent<Image>().color = new Color32(255, 255, 225, 100);
            }
        }
    }

    public void ResetSkills()
    {
        string[] lines = new string[3];
        for (int i = 0; i < 3; i++)
        {
            lines[i] = "#";
            abilitiesList[i].tag = "Untagged";
            abilitiesList[i].GetComponent<Image>().color = Color.white;
            abilitiesList[i].GetComponentInChildren<Text>().text = "Skill";
        }
        File.WriteAllLines(Directory.GetCurrentDirectory() + @"\__TextFiles\ActiveAbilitySave.sav", lines);
    }
}
