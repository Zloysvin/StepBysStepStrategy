using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    [SerializeField]  UpgradePoints Points = new UpgradePoints();
    public Text pointsText;
    public Text[] selectedSkills = new Text[3];

    private void Start()
    {
        ChangeSkills();
        ReadJson();    
        Debug.Log(Points.activeSkills[1]);
        File.WriteAllText(Application.persistentDataPath + "/PointsData.json", JsonUtility.ToJson(Points));
    }

    public void AddPoint()
    {
        Points.value += 1;
    }

    public void ChangeSkills()
    {
        for (int i = 0; i < Points.activeSkills.Length; i++)
        {
            Points.activeSkills[i] = selectedSkills[i].text;
        }
    }

    public void SaveIntoJson()  
    {
        string jsonSave = "";
        JsonUtility.FromJson<UpgradePoints>(jsonSave);
        string points = JsonUtility.ToJson(Points);
        File.WriteAllText(Application.persistentDataPath + "/PointsData.json", points);
    }

    public void ReadJson()
    {
        string pointsStr = File.ReadAllText(Application.persistentDataPath + "/PointsData.json");
        Points = JsonUtility.FromJson<UpgradePoints>(pointsStr);
        if (pointsText != null)
        {
            pointsText.text = "Upgrade points: " + Points.value.ToString();
            for (int i = 0; i < 3; i++)
            {
                selectedSkills[i].text = Points.activeSkills[i];
            }
        }
    }
}

[System.Serializable]
public class UpgradePoints
{
    public int value;
    public string[] activeSkills = new string[3];
}
