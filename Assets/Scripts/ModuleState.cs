using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleState : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

    //    myRB = GetComponent<Rigidbody>();

    //    myRB.Sleep();
        

        myCol = GetComponent<MeshCollider>();

        
    }

    private Rigidbody myRB;
    private MeshCollider myCol;

    // Update is called once per frame
    void Update()
    {
        
    }



}
