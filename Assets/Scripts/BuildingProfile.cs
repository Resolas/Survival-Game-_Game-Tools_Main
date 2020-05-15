using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuildingProfile", order = 100)]
public class BuildingProfile : ScriptableObject
{

    public GameObject[] groundModules;
    public GameObject[] mainModules;
    public GameObject[] roofModules;


    [Range(1,20)]
    public int maxHeight = 5;

}
