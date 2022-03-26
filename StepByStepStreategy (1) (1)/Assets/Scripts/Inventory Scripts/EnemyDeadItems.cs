using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadItems : MonoBehaviour
{
    public string Type = "";
    public List<string> items = new List<string>();
    void Update()
    {
        if (Type != "")
        {
            switch (Type)
            {
                case "Enemy":
                    Debug.Log("ADD");
                    items.Add("HealthPotion");
                    items.Add("Helmet");
                    items.Add("StartBodyArmor");
                    items.Add("StartBoots");
                    items.Add("DickHelmet");
                    break;
            }
            Debug.Log(GetComponent<ItemsHolder>());
            gameObject.GetComponent<ItemsHolder>().items = items;
            enabled = false;
        }
    }
}
