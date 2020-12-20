using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public List<Item> mobs = new List<Item>();
}

[System.Serializable]
public class Item
{
    public string nameMob;
    public GameObject prefab;
    public int chancePer;
    public float countHP;
    public float speed;
    public float countDam;
    public int deadPoints;
}
