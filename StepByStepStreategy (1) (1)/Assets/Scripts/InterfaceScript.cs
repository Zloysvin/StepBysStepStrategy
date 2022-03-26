using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceScript : MonoBehaviour
{
    public GameObject panel;
    public GameObject cameraObj;
    public GameObject playerUI;
    public GameObject inventoryWindow;
    public GameObject inventoryContentView;
    public GameObject InventoryCell;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !panel.activeSelf)
        {
            panel.SetActive(true);
            playerUI.SetActive(false);
            inventoryWindow.SetActive(false);
            cameraObj.GetComponent<RaycastMove>().enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && panel.activeSelf)
        {
            panel.SetActive(false);
            playerUI.SetActive(true);
            cameraObj.GetComponent<RaycastMove>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.I) && !inventoryWindow.activeSelf)
        {
            inventoryWindow.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryWindow.SetActive(false);
        }      
    }

    public void ButtonOnClickTest()
    {
        Debug.Log("Ok");
    }

    public void OnButtonLevelStart()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void OnButtonSceneChange()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OnButtonResumeGame()
    {
        panel.SetActive(false);
        playerUI.SetActive(true);
        cameraObj.GetComponent<RaycastMove>().enabled = true;
    }      
    public void OnButtonUpgradeOpen()
    {
        SceneManager.LoadScene("PlayerUpgradeSystemScene");
    }
}
