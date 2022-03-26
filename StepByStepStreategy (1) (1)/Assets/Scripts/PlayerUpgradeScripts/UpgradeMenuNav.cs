using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenuNav : MonoBehaviour
{
    public GameObject CombatPanel;
    public GameObject MovementPanel;
    public GameObject HealthPanel;
    public GameObject MainPanel;
    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }


    public void SwitchScenes()
    {      
            SceneManager.LoadScene("SampleScene");        
    }

    public void PanelManagement()
    {
        if (gameObject.name == "CombatButton")
        {
            CombatPanel.SetActive(true);
            MainPanel.SetActive(false);
        }
        else if (gameObject.name == "MovementButton")
        {
            MovementPanel.SetActive(true);
            MainPanel.SetActive(false);
        }
        else
        {
            HealthPanel.SetActive(true);
            MainPanel.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        MainPanel.SetActive(true);
        CombatPanel.SetActive(false);
        HealthPanel.SetActive(false);
        MovementPanel.SetActive(false);

    }
}
