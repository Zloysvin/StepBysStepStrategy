using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPersonaHandler : MonoBehaviour
{
    public string Type;
    void Start()
    {
        if(Type == "Enemy")
        {
            name = transform.name;
        }
    }
}
