using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int foodCount = 0, waterCount = 0, medKitCount = 0, ammo = 0, keyCount = 0;  // Note: ammo will be directly added to total

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerStats>().foodCount += foodCount;
            other.GetComponent<PlayerStats>().waterCount += waterCount;
            other.GetComponent<PlayerStats>().medKitCount += medKitCount;
            other.GetComponent<PlayerStats>().ammo += ammo;
            other.GetComponent<PlayerStats>().keyCount += keyCount;

            Destroy(gameObject);

        }
    }
}
