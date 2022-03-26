using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FuckJSON : MonoBehaviour
{
    string[] lines = new string[3];
    public GameObject[] SkillButtons = new GameObject[3];
    void Start()
    {
        lines = File.ReadAllLines(@"__TextFiles\ActiveAbilitySave.sav");
        for (int i = 0; i < 3; i++)
        {
            if(lines[i] != "#")
                SkillButtons[i].GetComponent<Button>().GetComponentInChildren<Text>().text = lines[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
