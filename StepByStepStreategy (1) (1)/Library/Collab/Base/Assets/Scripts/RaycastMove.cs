using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastMove : MonoBehaviour
{
    bool CanRaycast = true;
    bool CorpseOpened = false;

    public int f;                             
    int QuatTurns = 4;


    #region Materials
    public Material Base;           //Это базовая текстура т.е. пустая клетка
    public Material Posible;        //Это текстура клетки на которую можно перейти (в простонародье "подсвечивается")
    public Material AttackMaterial;
    public Material Timed;
    #endregion

    #region GameObjects
    public GameObject Player;   //Наш игрок. нужен, потому что 1) мы его перемещаем 2) от него мы прощитываем, на какие дальнейшие клетки можно перейти
    public GameObject HPpanel;
    public GameObject ButtonPanel;
    public GameObject Collect;
    public GameObject EnemyButton;
    public GameObject HealthBar;
    public GameObject Corpse;
    public GameObject PickLootMenu;
    public GameObject ItemOfResoureses;
    GameObject TurnButton;
    GameObject[] Enemies;
    GameObject[] Tiles;
    GameObject[] DeadInventoryChildren;
    GameObject item;
    List<GameObject> Corpses = new List<GameObject>();
    List<GameObject> Targets = new List<GameObject>();
    #endregion

    Transform MovementTarget;

    #region UI
    public Image Energy;

    public Button Attack;

    public Canvas canvas;
    #endregion

    public Camera cam;              //Камера нужна для Рейкаста

    System.Random rnd = new System.Random();
    void Start()
    {
        ChangeTiles(Player.transform, Posible, 3);     //В начале вызывается этот метод, чтобы сразу определить на какие клетки можно ходить. Это нужно, ибо с самого начала нету подсвечиваемых клеток и мы тупа не можем ходить
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Energy = GameObject.FindGameObjectWithTag("Energy").GetComponent<Image>();
        Tiles = GameObject.FindGameObjectsWithTag("Tile");
        TurnButton = GameObject.FindGameObjectWithTag("Turn");
        TurnButton.SetActive(false);
        SpawnEnemyLinkedButtons();
    }
    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && CanRaycast)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {

                if (hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture == Posible.mainTexture)       //Если текстура той клеточки на которую мы нажали является текстурой "подсвеченной клеточки", то надо: 1) заменить подсвеченные клеточки на обычные 2) передвинуть Игрока 3)  подсветить новые клеточки
                {
                    foreach (GameObject Tile in Tiles)
                    {
                            Tile.GetComponent<Renderer>().material = Base;
                    }
                    MovementTarget = hit.collider.transform;
                    DeadInventoryChildren = new GameObject[0];
                    Corpses.Clear();
                    Targets.Clear();
                    Attack.gameObject.SetActive(false);
                    Collect.SetActive(false);
                    PickLootMenu.SetActive(false);
                    Player.transform.position = MovementTarget.position;        //Перемещаем Игрока на координату клетки (надо будет потом переделать)
                    Player.transform.Translate(Vector3.up);         //Поднимает на 1 вверх, ибо "у" у клетки = 0, а у Игрока = 1 (надо будет потом переделать)
                    ChangeTiles(Player.transform, Posible, f);         //Подсвечиваем новые клетки
                    Energy.fillAmount += 0.1f;
                    QuatTurns--;
                    if (QuatTurns == 0)
                    {
                        TurnButton.SetActive(true);
                        CanRaycast = false;
                        ColorTiles(Posible, Timed);
                    }
                }
                else if (hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture == AttackMaterial.mainTexture)
                {
                    foreach (GameObject Enemy in Enemies)
                    {
                        if (hit.transform.position.x == Enemy.transform.position.x && hit.transform.position.z == Enemy.transform.position.z) 
                        {
                            Image img = Enemy.GetComponentInChildren<EnemyHPScript>().enemyHPbar.transform.GetChild(1).gameObject.GetComponent<Image>();
                            img.fillAmount -= 0.7f;
                            if (img.fillAmount <= 0)
                            {
                                GameObject CC =  Instantiate(Corpse, Enemy.transform.position, Quaternion.identity);
                                CC.GetComponent<EnemyDeadItems>().Type = Enemy.GetComponent<EnemyPersonaHandler>().Type;
                                Destroy(Enemy.GetComponent<EnemyHPScript>().enemyHPbar);
                                Destroy(Enemy);
                            }
                            break;
                        }
                    }
                    foreach (GameObject Tile in Tiles)
                    {
                    if(Tile.GetComponent<Renderer>().material.mainTexture == AttackMaterial.mainTexture)
                        Tile.GetComponent<Renderer>().material = Base;
                    }
                    TurnButton.SetActive(true);
                    CanRaycast = false;
                    ColorTiles(Posible, Timed);
                }
            }
        }
        if (Collect.activeSelf &&Input.GetKeyDown(KeyCode.E))
        {
                if (!CorpseOpened)
                    CorpseOpened = true;
                else
                    CorpseOpened = false;
                PickLootMenu.SetActive(true);
                var rsc = Corpses[0].GetComponent<ItemsHolder>().items;
                for (int i = 0; i < rsc.Count; i++)
                {
                    Debug.Log(rsc[i]);
                    item = Instantiate(ItemOfResoureses);
                    item.GetComponent<Item>().SpriteName = rsc[i];
                    string Path = rsc[i];
                    item.GetComponent<Image>().sprite = Resources.Load<Sprite>(rsc[i]);
                    item.GetComponent<Item>().InPlayerInventory = true;
                    item.transform.SetParent(PickLootMenu.transform, false);
                }
                DeadInventoryChildren = GameObject.FindGameObjectsWithTag("EnergyPotion");
                if (!CorpseOpened)
                {
                    for (int i = 0; i < DeadInventoryChildren.Length; i++)
                    {
                        Destroy(DeadInventoryChildren[i]);
                    }
                }
        }
    }
    public void Turn()
    {
        Enemies = new GameObject[0];
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in Enemies)
        {
                MoveEnemy(Enemy);
        }
        TurnButton.SetActive(false);
        CanRaycast = true;
        foreach (GameObject Tile in Tiles)
        {
            Tile.GetComponent<Renderer>().material = Base;
        }
        QuatTurns = 4;
        ChangeTiles(Player.transform, Posible, f);
    }
    void ChangeTiles(Transform Origin, Material material, int n)
    {
        #region Описание[v 1.0.]
        ///Опишу метод здесь, ибо потом еще буду вносить в него изменения
        ///по сути, этот метод заменяет у всех клеточек, которые находятся вокруг клеточки, которая находится под игроком, материал на данный тобой
        ///Работает очень просто
        ///1) Опсукает луч из Игрока(переменная Origin) вниз. Попадает в нижний объект, который потом переходит в Raycast hit и потом в переменную Origin. Сделал такую хню ибо удобнее обращаться
        ///2) Запускает 4 аналогичных ifa с использованием Рейкаста в 4 стороны (вверх, вниз, вправо, влево) и заменяет их материал на тот который я "дал" методу
        ///3)... Пока что все
        ///
        ///Надеюсь ты понял)))
        #endregion
        #region Описание[v 1.1.]
        ///Короче, добавил дополнительную проверку на окружающие игрока объекты. 
        ///Сделано это было с целью не дать игроку физическую возможнсть войти в припятствие
        ///До этого код в "ifах" состоял из одной строки, а теперь их там 13))0)
        ///Но пока я не придумал других идей как это реализовать более оптимально и я ужасно хочу спаааааааааааааААаааАААаААать!!))))
        ///Завтра, спишимся и я тебе поясню за каждую деталь кода, если не будешь понимать)
        #endregion
        RaycastHit hit;
        if (Physics.Raycast(new Ray(Origin.transform.position, Vector3.down), out hit))
        {
            RaycastHit hitC = hit;
            for (int i = 1; i < n; i++)
            {
                hit = CheckTiles(hit.transform, Player.transform, Vector3.left, i);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                }
                else
                    break;
            }
            hit = hitC;
            for (int i = 1; i < n; i++)
            {
                hit = CheckTiles(hit.transform, Player.transform, Vector3.right, i);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                }
                else
                    break;
            }
            hit = hitC;
            for (int i = 1; i < n; i++)
            {
                hit = CheckTiles(hit.transform, Player.transform, Vector3.forward, i);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                }
                else
                    break;
            }
            hit = hitC;
            for (int i = 1; i < n; i++)
            {
                hit = CheckTiles(hit.transform, Player.transform, Vector3.back, i);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                }
                else
                    break;
            }
        }
    }
    void MoveEnemy(GameObject Enemy)
    {
        RaycastHit hit;
        Transform transform1 = Enemy.transform;
        Transform transform2;
    here1:
        int Dir = rnd.Next(3);
        Vector3 Direction = Vector3.zero;
        if (Dir == 0)
            Direction = Vector3.forward;
        if (Dir == 1)
            Direction = Vector3.right;
        if (Dir == 2)
            Direction = Vector3.back;
        if (Dir == 3)
            Direction = Vector3.left;
        if (Physics.Raycast(new Ray(transform1.position, Vector3.down), out hit))
        {
            transform2 = hit.transform;
            if (Physics.Raycast(new Ray(transform2.position, Direction), out hit))
            {
                RaycastHit hitC = CheckTiles(transform2, transform1, Direction, 1);
                if (hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture != Posible.mainTexture && hitC.collider != null)
                {
                    Enemy.transform.position = new Vector3(hit.transform.position.x, 1, hit.transform.position.z);
                }
                else
                {
                    goto here1;
                }
            }
        }
    }
    RaycastHit CheckTiles(Transform Origin, Transform OriginCopy, Vector3 Direction, int n)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(Origin.transform.position, Direction), out hit))
        {
            RaycastHit hitC;
            Physics.Raycast(new Ray(OriginCopy.transform.position, Direction), out hitC);
            if (hitC.distance != 0)
            {
                float d1 = hitC.transform.position.x - OriginCopy.position.x;
                float d2 = hitC.transform.position.z - OriginCopy.position.z;
                if (d1 != n && d1 != -n && d2 != n && d2 != -n)
                {
                    if (hitC.transform.tag == "Enemy")
                    {
                        Debug.Log(OriginCopy.name + " " + hitC.transform.name + " " + (d1 != n && d1 != -n && d2 != n && d2 != -n));
                        Debug.Log(hitC.transform.name + " " + n + " x " + d1 + " y " + d2);
                        Debug.Log(hit.transform.position);
                    }
                    return hit;
                }
                else if(OriginCopy.tag != "Enemy" && hitC.transform.tag == "Enemy")
                {
                    Attack.gameObject.SetActive(true);
                    Targets.Add(hitC.collider.gameObject);
                }
                else if (hitC.transform.tag == "Corpse")
                {
                    Collect.SetActive(true);
                    Corpses.Add(hitC.transform.gameObject);
                }
                if (d1 != 0 && d2 != 0)
                {
                    Attack.gameObject.SetActive(false);
                    return hit;
                }
            }
            else
            {
                return hit;
            }
        }
        return new RaycastHit();
    }
    void CheckTilesForEnemies(Transform Origin, Vector3 Direction)
    {
        for (int i = 1; i <= 5; i++)
        {
            RaycastHit EnemyHit;
            RaycastHit hit;
            if (Physics.Raycast(new Ray(Origin.transform.position, Direction), out hit))
            {
                Origin = hit.collider.transform;
                if (Physics.Raycast(new Ray(Player.transform.position, Direction), out EnemyHit))
                {
                    if (Origin.position.x == EnemyHit.transform.position.x && Origin.position.z == EnemyHit.transform.position.z && EnemyHit.transform.gameObject.tag == "Enemy")
                    {
                        Origin.gameObject.GetComponent<Renderer>().material = AttackMaterial;
                    }
                }
            }
            else
                break;
        }
    }
    void ColorTiles(Material target, Material change)
    {
        foreach (GameObject Tile in Tiles)
        {
            if (Tile.GetComponent<Renderer>().material.mainTexture == target.mainTexture)
            {
                Tile.GetComponent<Renderer>().material = change;
            }
        }
    }
    public void SpawnEnemyLinkedButtons()
    {

        foreach (GameObject Enemy in Enemies)
        {
            
            GameObject newbutton = Instantiate(EnemyButton);
            GameObject HPbar = Instantiate(HealthBar);
            Enemy.GetComponent<EnemyHPScript>().enemyHPbar = HPbar;
            HPbar.transform.SetParent(HPpanel.transform, false);

            newbutton.transform.localScale = new Vector3(1, 1, 1);
            newbutton.GetComponent<EnemyButton>().Enemy = Enemy;
            newbutton.transform.SetParent(ButtonPanel.transform, false);
        }
    }
    public void DashAbilityMethod()
    {
        RaycastHit hit;
        if(Energy.fillAmount >=0.3f )
        {
            Energy.fillAmount -= 0.3f;
            if (Physics.Raycast(new Ray(Player.transform.position, Vector3.down), out hit))
            {
                RaycastHit hitC = hit;
                for (int i = 1; i < 6; i++)
                {
                    hit = CheckTiles(hit.transform, Player.transform, Vector3.left, i);
                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                    }
                    else
                        break;
                }
                hit = hitC;
                for (int i = 1; i < 6; i++)
                {
                    hit = CheckTiles(hit.transform, Player.transform, Vector3.right, i);
                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                    }
                    else
                        break;
                }
                hit = hitC;
                for (int i = 1; i < 6; i++)
                {
                    hit = CheckTiles(hit.transform, Player.transform, Vector3.forward, i);
                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                    }
                    else
                        break;
                }
                hit = hitC;
                for (int i = 1; i < 6; i++)
                {
                    hit = CheckTiles(hit.transform, Player.transform, Vector3.back, i);
                    if (hit.collider != null)
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().material = Posible;
                    }
                    else
                        break;
                }
            }
        }
    }
    public void AttackAbilityMethod()
    {
        foreach (GameObject Target in Targets)
        {
            Image img = Target.GetComponentInChildren<EnemyHPScript>().enemyHPbar.transform.GetChild(1).gameObject.GetComponent<Image>();
            img.fillAmount -=0.5f;
            CanRaycast = false;
            TurnButton.SetActive(true);
            ColorTiles(Posible, Timed);
            if (img.fillAmount <= 0)
            {
                Instantiate(Corpse, Target.transform.position, Quaternion.identity).GetComponent<EnemyDeadItems>().Type = Target.GetComponent<EnemyPersonaHandler>().Type;
                GameObject TargetC = Target;
                Targets.Remove(Target);
                Destroy(TargetC.GetComponent<EnemyHPScript>().enemyHPbar);
                Destroy(TargetC);
            }
        }
    }
    public void RangeAttackAbilityMethod()
    {
        RaycastHit hit;
        if(Physics.Raycast(new Ray(Player.transform.position, Vector3.down), out hit))
        {
            Transform Origin = hit.collider.transform;
            CheckTilesForEnemies(Origin, Vector3.left);
            CheckTilesForEnemies(Origin, Vector3.right);
            CheckTilesForEnemies(Origin, Vector3.forward);
            CheckTilesForEnemies(Origin, Vector3.back);
        }
    }
}
