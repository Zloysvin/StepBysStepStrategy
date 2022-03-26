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
        //типичный пример шутки "Если оно работает то не трогай"
            GameObject Player = GameObject.FindGameObjectWithTag("MainCamera");
            int index = Player.GetComponent<Inventory>().Items.IndexOf(GetComponent<Item>().SpriteName);
            GameObject ItemInSlot;
            GetComponent<Item>().InPlayerInventory = false;
            Player.GetComponent<Inventory>().Items.Remove(GetComponent<Item>().SpriteName);
            Player.GetComponent<Inventory>().ItemsSprites.Remove(Player.GetComponent<Inventory>().ItemsSprites[index]);
            if (GetComponent<Item>().IsHelmet)
            {
                ItemInSlot = GameObject.FindGameObjectWithTag("NonHeadInventoryIten");
                if (ItemInSlot == null)
                {
                    transform.SetParent(GameObject.FindGameObjectWithTag("HelmetSlot").transform, false);
                    transform.tag = "NonHeadInventoryIten";
                }
                else
                {
                    Player.GetComponent<Inventory>().Items.Add(ItemInSlot.GetComponent<Item>().SpriteName);
                    Player.GetComponent<Inventory>().Changed = true;
                    Player.GetComponent<Inventory>().ItemsSprites.Add(ItemInSlot.GetComponent<Image>().sprite);
                    transform.SetParent(GameObject.FindGameObjectWithTag("HelmetSlot").transform, false);
                    ItemInSlot.transform.tag = "InventoryItem";
                    transform.tag = "NonHeadInventoryIten";
                }
            }
            if (GetComponent<Item>().isBodyArmor)
            {
                ItemInSlot = GameObject.FindGameObjectWithTag("NonBodyInventoryIten");
                if (ItemInSlot == null)
                {
                    transform.SetParent(GameObject.FindGameObjectWithTag("BodyArmorSlot").transform, false);
                    transform.tag = "NonBodyInventoryIten";
                }
                else
                {
                    Player.GetComponent<Inventory>().Items.Add(ItemInSlot.GetComponent<Item>().SpriteName);
                    Player.GetComponent<Inventory>().Changed = true;
                    Player.GetComponent<Inventory>().ItemsSprites.Add(ItemInSlot.GetComponent<Image>().sprite);
                    transform.SetParent(GameObject.FindGameObjectWithTag("BodyArmorSlot").transform, false);
                    ItemInSlot.transform.tag = "InventoryItem";
                    transform.tag = "NonBodyInventoryIten";
                }
            }
            if (GetComponent<Item>().isBoots) 
            {
                ItemInSlot = GameObject.FindGameObjectWithTag("NonLegsInventoryIten");
                if (ItemInSlot == null)
                {
                    transform.SetParent(GameObject.FindGameObjectWithTag("BootsSlot").transform, false);
                    transform.tag = "NonLegsInventoryIten";
                }
                else
                {
                    Player.GetComponent<Inventory>().Items.Add(ItemInSlot.GetComponent<Item>().SpriteName);
                    Player.GetComponent<Inventory>().Changed = true;
                    Player.GetComponent<Inventory>().ItemsSprites.Add(ItemInSlot.GetComponent<Image>().sprite);
                    transform.SetParent(GameObject.FindGameObjectWithTag("BootsSlot").transform, false);
                    ItemInSlot.transform.tag = "InventoryItem";
                    transform.tag = "NonLegsInventoryIten";
                }
            }
        }
    }
}
