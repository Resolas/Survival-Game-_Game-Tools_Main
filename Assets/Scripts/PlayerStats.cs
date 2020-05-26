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
    public float ammo = 100f;
    public float radiation = 0f;
    public bool isDead = false;

    public float hungerRate = 0.3f;
    public float thirstRate = 0.15f;
    // Nuke Variables
    

    public static bool nuke = false;
    public static float nukeDropTimer = 60f;
    public float maxNukeTimer = 60f;
    public GameObject nukeObject;
    public static GameObject targetScout;


    private bool isRunning;
    public float runThirstMult = 2f;
    private float currentThirstMult = 1f;
    

    #endregion

  

    void Update()
    {
        StatsUpdater();

        nukeFunction();


    }

    public void StatsUpdater()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (health <= 0 || radiation >= 100)
        {
            isDead = true;
        }

        if (food >= 0) food -= hungerRate * Time.deltaTime;


        if (isRunning)
        {
            currentThirstMult = runThirstMult;
        }
        else
        {
            currentThirstMult = 1f;
        }

        if (water >= 0) water -= thirstRate * currentThirstMult * Time.deltaTime;


        if (food <= 0 || water <= 0) health -= 0.3f * Time.deltaTime;

    }

    public void nukeFunction()
    {
        if (targetScout == null) // Reset Everything if scout is destroyed or missing
        {
            nuke = false;
            nukeDropTimer = maxNukeTimer;

        }

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
                Debug.Log("TIMER RESET");
            }
        }

        
    }
    

}
