using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    [Header("Player Settings")]
    public float health = 100f;
    public float food = 100f;
    public float water = 100f;
    public float radiation = 0f;
    public bool isDead = false;
    // Nuke Variables
    

    public static bool nuke = false;
    public static float nukeDropTimer = 60f;
    public float maxNukeTimer = 60f;
    public GameObject nukeObject;
    public static GameObject targetScout;

    #endregion

  

    void Update()
    {

        nukeFunction();


    }

    public void nukeFunction()
    {
        if (nuke)
        {
            if (nukeDropTimer >= 0f)
            {
                nukeDropTimer -= 1 * Time.deltaTime;
            }

            if (nukeDropTimer <= 0f && targetScout != null)
            {
                // Spawns Nuke 3000 Units Above Triggered Scout
                var fireNuke = Instantiate(nukeObject, new Vector3(targetScout.transform.position.x, 3000, targetScout.transform.position.z), Quaternion.identity);

                nukeDropTimer = maxNukeTimer;
                targetScout = null;
            }
        }

        
    }
    

}
