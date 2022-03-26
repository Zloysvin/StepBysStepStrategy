using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public void Clickk()
    {
        Debug.Log("Item in Inventory " + GetComponent<Item>().InPlayerInventory);
        if (!GetComponent<Item>().InPlayerInventory)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("MainCamera");
            Player.GetComponent<Inventory>().Items.Add(GetComponent<Item>().SpriteName);
            Player.GetComponent<Inventory>().Changed = true;
            Player.GetComponent<Inventory>().ItemsSprites.Add(GetComponent<Image>().sprite);
            Destroy(gameObject);
        }
        else
        {

            if (GetComponent<Item>().IsHelmet)
            {
                transform.SetParent(GameObject.FindGameObjectWithTag("HelmetSlot").transform, false);
            }
            if (GetComponent<Item>().isBodyArmor)
            {
                transform.SetParent(GameObject.FindGameObjectWithTag("BodyArmorSlot").transform, false);
            }
            if (GetComponent<Item>().isBoots) {
                transform.SetParent(GameObject.FindGameObjectWithTag("BootsSlot").transform, false);
            }
        }
    }
}
