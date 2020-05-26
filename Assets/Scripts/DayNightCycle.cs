using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }
    public float rotSpeed = 1;
    private Transform myTransform;
    // Update is called once per frame
    void FixedUpdate()
    {
        myTransform.Rotate(0,rotSpeed * Time.deltaTime,0);
    }
}
