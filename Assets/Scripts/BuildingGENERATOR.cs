using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildingGENERATOR : MonoBehaviour
{
   public GameObject[] moduleGroundBlocks;
   public GameObject[] moduleMainBlocks;
   public GameObject[] moduleRoofBlocks;

    float minBuild;
    float maxBuild;
    float minLimit;
    float maxLimit;

    int val;

    private Transform myPos;

    private int buildingHeight;

    [Header("Height Generation")]
    [Range(1,50)]
    public int hMax;
    [Range(1,50)]
    public int hMin;
    /*
        private void OnGUI()
        {
            EditorGUILayout.LabelField("Min Value", minBuild.ToString());
            EditorGUILayout.LabelField("Max Value", maxBuild.ToString());
         val = Mathf.RoundToInt(EditorGUILayout.MinMaxSlider(ref minBuild, ref maxBuild, minLimit, maxLimit));  
        }
        */
    
    
    private void Awake()
    {


        myPos = GetComponent<Transform>();
        buildingHeight = Random.Range(hMin,hMax);
        
        for (int i = 0; i < buildingHeight; i ++)
        {
            int rngGround = Random.Range(0, moduleGroundBlocks.Length);
            int rngMain = Random.Range(0, moduleMainBlocks.Length);
            int rngRoof = Random.Range(0, moduleRoofBlocks.Length);

            

            if (i == 0)
            {
              var myGroundModule = Instantiate(moduleGroundBlocks[rngGround], new Vector3(myPos.position.x, myPos.position.y + (80 * i), myPos.position.z), Quaternion.identity);
              //  myGroundModule.transform.parent = gameObject.transform;
                myGroundModule = null;
            }

             if (i >= 1 && i < buildingHeight - 1)
            {
               var myMainModule = Instantiate(moduleMainBlocks[rngMain], new Vector3(myPos.position.x, myPos.position.y + (80 * i), myPos.position.z), Quaternion.identity);
              //  myMainModule.transform.parent = gameObject.transform;
                myMainModule = null;
            }
            
             if (i == buildingHeight - 1)
            {
               var myRoofModule = Instantiate(moduleRoofBlocks[rngRoof], new Vector3(myPos.position.x, myPos.position.y + (80 * i), myPos.position.z), Quaternion.identity);
              //  myRoofModule.transform.parent = gameObject.transform;     // parenting spawns another building in?????
                myRoofModule = null;
            }
            

        }

    }
    /*
    private void OnCollisionStay(Collision collision)
    {
        var other = collision.gameObject;

        if (other.tag == "BuildingGenerator")
        {
            Debug.Log("Object DELETED");
            Destroy(other);
            Destroy(this.gameObject);
        }
    }
    */

        
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BuildingGenerator")
        {
            Debug.Log("Object DELETED" + other);
            DestroyImmediate(other);
            DestroyImmediate(this);
        }
    }
    

}
