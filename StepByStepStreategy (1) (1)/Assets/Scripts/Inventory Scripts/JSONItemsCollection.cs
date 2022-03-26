using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JSONItemsCollection : MonoBehaviour
{
    public ItemDataCollection JsonCollection = new ItemDataCollection();
    void Start()
    {
        string JSON = File.ReadAllText(Application.persistentDataPath + "/ItemList.json");
        Debug.Log(JSON);
        JsonCollection = JsonConvert.DeserializeObject<ItemDataCollection>(JSON);
        Debug.Log(JsonCollection.IDC.Count);
        for (int i = 0; i < JsonCollection.IDC.Count; i++)
        {
            Debug.Log(JsonCollection.IDC[i].SpriteName1);
        }
    }
    public ItemData FindItemByName(string name)
    {
        for (int i = 0; i < JsonCollection.IDC.Count; i++)
        {
            if (JsonCollection.IDC[i].SpriteName1 == name)
            {
                return JsonCollection.IDC[i];
            }
        }
        return null;
    }
    public class ItemDataCollection
    {
         public List<ItemData> IDC;
    }
    public class ItemData
    {
        bool Usable = false;
        string SpriteName = "";
        bool IsAttackItem = false;
        int Damage = 0;
        bool isBodyArmor = false;
        bool IsHelmet = false;
        bool isBoots = false;
        int Defense = 0;
        bool IsHealingItem = false;
        float HealthRegenAmount = 0;

        public bool Usable1 { get => Usable; set => Usable = value; }
        public string SpriteName1 { get => SpriteName; set => SpriteName = value; }
        public bool IsAttackItem1 { get => IsAttackItem; set => IsAttackItem = value; }
        public int Damage1 { get => Damage; set => Damage = value; }
        public bool IsBodyArmor { get => isBodyArmor; set => isBodyArmor = value; }
        public bool IsHelmet1 { get => IsHelmet; set => IsHelmet = value; }
        public bool IsBoots { get => isBoots; set => isBoots = value; }
        public int Defense1 { get => Defense; set => Defense = value; }
        public bool IsHealingItem1 { get => IsHealingItem; set => IsHealingItem = value; }
        public float HealthRegenAmount1 { get => HealthRegenAmount; set => HealthRegenAmount = value; }
    }
}
