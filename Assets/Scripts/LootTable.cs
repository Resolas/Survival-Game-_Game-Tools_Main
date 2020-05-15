using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LootTable", order = 100)]
public class LootTable : ScriptableObject
{

    public GameObject[] lootList;

}
