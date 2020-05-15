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

    void Start()
    {
        


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

    }


}
