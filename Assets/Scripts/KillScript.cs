using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("DESTROY");
        Destroy(collision.gameObject.GetComponent<GameObject>());
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("DESTROY2");
        Destroy(other.gameObject);
    }

    
}
