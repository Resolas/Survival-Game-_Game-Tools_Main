using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myVector = GetComponent<Vector3>();    
    }

    Vector3 myVector;
    public float speed = 10f;
    public float explodeDist = 100f;
    public GameObject explosion;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.down * speed;

        Explode();
    }

    public void Explode()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
      //  Vector3 rayPos;
        Debug.DrawLine(transform.position, Vector3.down * explodeDist, Color.red);
        

        if (Physics.Raycast(transform.position,Vector3.down, out hit,explodeDist,mask))
        {
            Debug.Log("TESTRAY");
            PlayerStats.nuke = false;
            Instantiate(explosion,gameObject.transform.position,Quaternion.identity);
            

            Destroy(gameObject);
        }
    }

}
