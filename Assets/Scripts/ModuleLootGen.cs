using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLootGen : MonoBehaviour
{

    public Transform[] itemGenPos;

    public LootTable myLootTable;
    private int rngLoot;
    [Range(0,100)]
    public int spawnRate;

    private void Awake()
    {

        

        for (int i = 0; i < itemGenPos.Length; i ++)    // Will iterate through each position and spawn an item there
        {
            rngLoot = Random.Range(0,myLootTable.lootList.Length);
            int chance = Random.Range(0,100);
            if (chance < spawnRate)
            {
                Instantiate(myLootTable.lootList[rngLoot], itemGenPos[i].position,Quaternion.identity);
            }



        }

    }


}
