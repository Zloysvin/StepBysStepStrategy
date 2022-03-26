using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool InPlayerInventory = false;
    public bool Usable = false;
    public bool IsAttackItem = false;
    public int Damage = 0;
    public bool IsHealingItem = false;
    public float HealthRegenAmount = 0;
    public string SpriteName = "";
    public void TipaJson()
    {
        switch (SpriteName)
        {
            case "HealthPotion":
                Usable = false;
                IsAttackItem = false;
                Damage = 0;
                IsHealingItem = true;
                HealthRegenAmount = 0.3f;
                break;
            case "loh":
                Usable = true;
                IsAttackItem = false;
                Damage = 0;
                IsHealingItem = false;
                HealthRegenAmount = 0;
                break;
        }
    }
}
