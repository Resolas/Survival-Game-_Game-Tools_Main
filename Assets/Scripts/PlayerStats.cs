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
    public GameObject gameOverScreen;
    public int currentSelection = 0;

    public int foodCount = 0, waterCount = 0, medKitCount = 0, ammoCount = 0, keyCount = 0;

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


    public GameObject bullet;
    public Transform firePoint;

    public GameObject[] icons = new GameObject[3];
    public GameObject iconHighlight;
    private float cooldown = 0;
    public float setCooldown = 3;

    #endregion

  

    void Update()
    {
        StatsUpdater();

        ConsumeItem();


    }

    private void FixedUpdate()
    {
        nukeFunction();
        
        if (Input.GetMouseButton(0))
        {
            FireGun();
        }
        if (cooldown >= 0) cooldown -= 1 * Time.deltaTime;
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

        if (isDead)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameOverScreen.SetActive(false);
           // Time.timeScale = 1;
        }
        // Max Values
        if (health > 100) health = 100;
        if (food > 100) food = 100;
        if (water > 100) water = 100;


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

    public void FireGun()
    {

        if (ammo > 0 && cooldown <= 0)
        {
            Instantiate(bullet,firePoint.position,firePoint.transform.rotation);
            ammo--;
            cooldown = setCooldown;
        }
        
    }

    public void ConsumeItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSelection = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSelection = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSelection = 2;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (currentSelection)
            {

                case 0: // Food
                    if (foodCount > 0)
                    {
                        food += 30f;
                        foodCount -= 1;
                    }
                    break;

                case 1: // Water
                    if (waterCount > 0)
                    {
                        water += 30f;
                        waterCount -= 1;
                    }
                    break;

                case 2: // Medkit
                    if (medKitCount > 0)
                    {
                        health += 30f;
                        medKitCount  -= 1;
                    }
                    break;

            }
        }

        for (int i = 0; i < icons.Length; i++)
        {
            if (i == currentSelection)
            {
                iconHighlight.transform.position = icons[i].transform.position;
            }
        }


    }


}
