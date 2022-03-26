using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool InPlayerInventory = false;
    public bool Usable = false;
    public string SpriteName = "";

    public bool IsAttackItem = false;
    public int Damage = 0;

    public bool IsHelmet = false;
    public bool IsChest = false;
    public bool IsLegs = false;
    public int Defense = 0; 

    public bool IsHealingItem = false;
    public float HealthRegenAmount = 0;
    public void TipaJson()
    {
        switch (SpriteName)
        {
            case "HealthPotion":
                Usable = false;

                IsHealingItem = true;
                HealthRegenAmount = 0.3f;
                break;
            case "loh":
                Usable = true;

                IsHelmet = true;
                Defense = 5;
                break;
        }
    }
}
