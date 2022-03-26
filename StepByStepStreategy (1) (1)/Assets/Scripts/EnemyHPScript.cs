using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{

    public GameObject enemyHPbar;

    
    void Update()
    {
        Vector3 barPos = Camera.main.WorldToScreenPoint(this.transform.position);
        enemyHPbar.transform.position = barPos;
    }
}
