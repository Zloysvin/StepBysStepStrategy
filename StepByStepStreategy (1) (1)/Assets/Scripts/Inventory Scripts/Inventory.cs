using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject Item;

    public GameObject ContentView;
    public GameObject PickLootMenu;
    public GameObject InventoryMenu;

    public bool Changed = false;

    public List<string> Items = new List<string>();
    public List<Sprite> ItemsSprites = new List<Sprite>();
    void Update()
    {
        //Debug.Log((ContentView == null) + " " + (InventoryMenu.activeSelf));
        if (InventoryMenu.activeSelf && Changed)
        {
            Debug.Log("I'm HERE");
            GameObject[] DeadInventoryChildren = GameObject.FindGameObjectsWithTag("InventoryItem");
            if (DeadInventoryChildren.Length > 0)
            {
                for (int i = 0; i < DeadInventoryChildren.Length; i++)
                {
                    Destroy(DeadInventoryChildren[i]);
                }
            }
            for (int i = 0; i < Items.Count; i++)
            {
                GameObject NewPlayerItem = Instantiate(Item);
                NewPlayerItem.tag = "InventoryItem";
                NewPlayerItem.GetComponent<Item>().SpriteName = Items[i];
                NewPlayerItem.GetComponent<Image>().sprite = ItemsSprites[i];
                NewPlayerItem.GetComponent<Item>().InPlayerInventory = true;
                NewPlayerItem.GetComponent<Item>().TipaJson();
                NewPlayerItem.transform.SetParent(ContentView.transform, false);
            }
            Changed = false;
        }
    }

    /*public void OnButtonAddEnergyPotion()
    {
        ContentView = GameObject.FindGameObjectWithTag("ContentView");
        GameObject newCell = Instantiate(EnergyPotion);
        newCell.transform.SetParent(ContentView.transform, false);
    }

    public void OnButtonHealthPotionAdd()
    {
        ContentView = GameObject.FindGameObjectWithTag("ContentView");
        GameObject newCell = Instantiate(HealthPotion);
        newCell.transform.SetParent(ContentView.transform, false);
    }*/
}
