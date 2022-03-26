using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    [SerializeField]  static UpgradePoints Points = new UpgradePoints();
    public Text pointsText;
    public Text[] selectedSkills = new Text[3];

    private void Start()
    {
        string points = JsonUtility.ToJson(Points);
        Points = JsonUtility.FromJson<UpgradePoints>(points);
        ReadSkills();
        ReadJson();
        ChangeSkills();
    }

    public void AddPoint()
    {
        Points.value += 1;
    }

    public void ChangeSkills()
    {
        if (SceneManager.GetActiveScene().name.Equals("PlayerUpgradeSystemScene"))
        {
            for (int i = 0; i < Points.activeSkills.Length; i++)
            {
                Points.activeSkills[i] = selectedSkills[i].text;
            }
        }       
    }

    public void ReadSkills()
    {
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            string pointsStr = File.ReadAllText(Application.persistentDataPath + "/PointsData.json");
            Points = JsonUtility.FromJson<UpgradePoints>(pointsStr); //points);
            for (int i = 0; i < Points.activeSkills.Length; i++)
            {
                selectedSkills[i].text = Points.activeSkills[i];
            }
        }         
    }


    public void SaveIntoJson()  
    {
       
        ChangeSkills();
        string points = JsonUtility.ToJson(Points);
        File.WriteAllText(Application.persistentDataPath + "/PointsData.json", points);
        print("saved-"+ points);
    }

    public void ReadJson()
    {
        string pointsStr = File.ReadAllText(Application.persistentDataPath + "/PointsData.json");
        Points = JsonUtility.FromJson<UpgradePoints>(pointsStr); //points);
        if (pointsText != null)
        {
            pointsText.text = "Upgrade points: " + Points.value.ToString();
            for (int i = 0; i < Points.activeSkills.Length; i++)
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
