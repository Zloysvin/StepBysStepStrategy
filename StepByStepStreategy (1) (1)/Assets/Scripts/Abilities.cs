using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public void OnAbility()
    {
        switch (GetComponent<Button>().GetComponentInChildren<Text>().text)
        {
            case "Dash":
                Shit1();
                break;
        }
    }
    void Shit1()
    {
        GameObject[] gbs = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            gbs[i] = Instantiate(GameObject.FindGameObjectWithTag("Enemy"));
            System.Random rnd = new System.Random();
            foreach (GameObject gb in gbs)
            {
                gb.transform.position = new Vector3(rnd.Next(15), 1, rnd.Next(15));
            }
        }
    }
}
