using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    public void Clickk()
    {
        if (!GetComponent<Item>().InPlayerInventory)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("MainCamera");
            Player.GetComponent<Inventory>().Items.Add(GetComponent<Item>().SpriteName);
            Player.GetComponent<Inventory>().Changed = true;
            Player.GetComponent<Inventory>().ItemsSprites.Add(GetComponent<Image>().sprite);
            Destroy(gameObject);
        }
        //else
        //{
        //    if (GetComponent<Item>().Usable)
        //    {
        //        if (gameObject.name == "Helmet") {
        //            gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("ContentView").transform, false);
        //        }
        //    }
        //}
    }
}
