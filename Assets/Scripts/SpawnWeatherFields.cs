using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeatherFields : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWeather",20,20);
    }

    public GameObject weather;



    // Update is called once per frame
    void FixedUpdate()
    {
          
    }

    void SpawnWeather()
    {
        float rngXPos = Random.Range(-5000,5000);
        float rngZPos = Random.Range(-5000,5000);

        Instantiate(weather, new Vector3(transform.position.x + rngXPos,transform.position.y,transform.position.z + rngZPos), this.transform.rotation);
    }
}
