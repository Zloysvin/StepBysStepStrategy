using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class Tools
{
    [MenuItem("Tools/Move Enemy Randomly")]
    public static void MoveEnemyRandomly()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        System.Random rnd = new System.Random();
        foreach(GameObject Enemy in Enemies)
        {
            Enemy.transform.position = new Vector3(rnd.Next(15), 1, rnd.Next(15));
        }
    }

    [MenuItem("Tools/Change Energy Value:)")]
    public static void ChangeValueOf()
    {
        Image img = GameObject.FindGameObjectWithTag("Energy").GetComponent<Image>();
        img.fillAmount-= 0.1f;
    }

    [MenuItem("Tools/XCOM camera position")]
    public static void CombatCamera()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 combatPos = player.transform.position + new Vector3(0, 2f, 0);
        

        if (cam.GetComponent<SmoothCameraMovement>().enabled == true && cam.transform.position != combatPos)
        {
            cam.GetComponent<SmoothCameraMovement>().enabled = false;
            cam.transform.position = combatPos;
            cam.transform.LookAt(GameObject.FindGameObjectWithTag("Enemy").transform);            
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, GameObject.FindGameObjectWithTag("Enemy").transform.position, -4);
        }
        else
        {
            cam.GetComponent<SmoothCameraMovement>().enabled = true;
            cam.transform.rotation = Quaternion.Euler(70, 0, 0);
        }
              
        
    }

    [MenuItem("Tools/Show Turn Button")]
    public static void ShowTurnButton()
    {
        GameObject Button = GameObject.FindGameObjectWithTag("TurnHandler").GetComponent<TurnButtonHandler>().Handler;
        Button.SetActive(true);
    }

}
