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
            Debug.Log(GetComponent<Item>().SpriteName + " IsHelmet " + GetComponent<Item>().IsHelmet);
            if (GetComponent<Item>().IsHelmet)
            {
                transform.SetParent(GameObject.FindGameObjectWithTag("HelmetSlot").transform);
            }
        }
    }
}
