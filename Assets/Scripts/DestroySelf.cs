using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        Invoke("_DestroySelf",timer);

        if (true) Invoke("_DestroyComp", timeDestroyComp);

    }

    private Rigidbody myRB;

    public float timer = 5f;
    public float timeDestroyComp = 2;

    public bool destroyRigidBody = false;

    void _DestroySelf()
    {
        Destroy(gameObject);
    }

    void _DestroyComp()
    {
        Destroy(this.myRB);
    }
}
