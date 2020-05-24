using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myPos = GetComponent<Vector3>();
    }
    private Vector3 myPos;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * 2;
    }
}
