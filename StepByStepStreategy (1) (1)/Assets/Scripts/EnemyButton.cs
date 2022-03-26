using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    public GameObject Enemy;

    public void CombatCamera()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 combatPos = player.transform.position + new Vector3(0, 2f, 0);
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

        if (cam.GetComponent<SmoothCameraMovement>().enabled == true && cam.transform.position != combatPos)
        {          
            cam.GetComponent<SmoothCameraMovement>().enabled = false;
            cam.GetComponent<RaycastMove>().enabled = false;
            cam.transform.position = combatPos;
            Transform EnemyTransform = Enemy.transform;
            cam.transform.LookAt(EnemyTransform);
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, EnemyTransform.position, -4);

        }
        else
        {
            cam.GetComponent<SmoothCameraMovement>().enabled = true;
            cam.GetComponent<RaycastMove>().enabled = true;
            cam.transform.rotation = Quaternion.Euler(70, 0, 0);
        }


    }
}
