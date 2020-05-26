using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementGen : MonoBehaviour
{

    

    

    [Header("Generation Settings")]
    
    [Range(0,100)]
    public int spawnRate = 50;
    public float baseX = 100f, baseY = 100f, baseZ = 100f;   // base value between each building
    public GameObject building;

    [Header("Rubble Settings")]
    [Range(0,100)]
    public int debrisSpawnRate = 50;
    public float debBaseX = 50f, debBaseY = 50f, debBaseZ = 50f;
    public GameObject[] debris;

    [Header("Enemy Settings")]
    [Range(0,100)]
    public int EnemySpawnRate = 50;
    public float EnbaseX = 100f, EnbaseY = 100f, EnbaseZ = 100f;
    public GameObject[] enemy;



    void Start()
    {


        #region Building Generator
        // BUILDING POSITION GENERATOR  
        for (int i = 0; i < 50; i ++)
        {


           // Debug.Log("TEST0");
            
            for (int j = 0; j < 50; j++)
            {

                int rng = Random.Range(0,100);
                

                
                if (rng < spawnRate)   // rng is less than spawnRate   Building will spawn
                {
                    float x = Random.Range(-75, 75);
                    float z = Random.Range(-75, 75);

                    
                    
                 //   if (Physics.Raycast(ray, out hit))

                    float xPos = (baseX * i) + x;
                    float zPos = (baseY * j) + z;

                    Vector3 rayPos = new Vector3(xPos,500,zPos);
                    LayerMask mask = LayerMask.GetMask("Ground");

                    
                    RaycastHit hit;  // Raycast hits ground gets position for Y allowing building objects to spawn on varying terrain

                    if (Physics.Raycast(rayPos, Vector3.down, out hit, 3000f,mask))
                    {
                        Instantiate(building, new Vector3(xPos,hit.point.y, zPos), Quaternion.identity);
                        
                    }

                    
                }
                
            }
            
        }
        #endregion


        #region Debris/Rubble Generator


        for (int i = 0; i < 100; i++)
        {

            for (int j = 0; j < 100; j++)
            {

                int rng = Random.Range(0,100);

                if (rng < debrisSpawnRate && debris != null)
                {

                    float x = Random.Range(-75, 75);
                    float z = Random.Range(-75, 75);

                    float xPos = (debBaseX * i) + x;
                    float zPos = (debBaseZ * j) + z;

                    int rngDebris = Random.Range(0,debris.Length);  // Picks a random existing in element size prefab/object to spawn

                    Vector3 rayPos = new Vector3(xPos, 500, zPos);
                    LayerMask mask = LayerMask.GetMask("Ground");


                    RaycastHit hit;  // Raycast hits ground gets position for Y allowing Debris objects to spawn on varying terrain

                    if (Physics.Raycast(rayPos, Vector3.down, out hit, 3000f, mask))
                    {
                        Instantiate(debris[rngDebris], new Vector3(xPos, hit.point.y, zPos), Quaternion.Euler(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

                    }

                }

            }
        }


        #endregion


        #region Enemy Generator

        for (int i = 0; i < 50; i++)
        {

            for (int j = 0; j < 50; j++)
            {

                int rng = Random.Range(0, 100);

                if (rng < EnemySpawnRate)
                {

                    float x = Random.Range(-75, 75);
                    float z = Random.Range(-75, 75);

                    float xPos = (EnbaseX * i) + x;
                    float zPos = (EnbaseZ * j) + z;

                       int rngEnemy = Random.Range(0, enemy.Length);  // Picks a random existing in element size prefab/object to spawn

                    Vector3 rayPos = new Vector3(xPos, 500, zPos);
                    LayerMask mask = LayerMask.GetMask("Ground");


                    RaycastHit hit;  // Raycast hits ground gets position for Y allowing Debris objects to spawn on varying terrain

                    if (Physics.Raycast(rayPos, Vector3.down, out hit, 3000f, mask))
                    {
                        Instantiate(enemy[rngEnemy], new Vector3(xPos, hit.point.y, zPos), Quaternion.identity);

                    }

                }

            }
        }

        #endregion

    }


}
