using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUse : MonoBehaviour
{
   public GameObject potion;
    public GameObject ContentView;

    

   public void OnButtonHealth()
    {
        
        if (gameObject.transform.parent.name == "PickLootMenu")
        {
            gameObject.transform.SetParent(ContentView.transform, false);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void OnButtonMana()
    {
        GameObject ContentView = GameObject.FindGameObjectWithTag("ContentView");
        if (gameObject.transform.parent.name == "PickLootMenu")
        {
            gameObject.transform.SetParent(ContentView.transform, false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
