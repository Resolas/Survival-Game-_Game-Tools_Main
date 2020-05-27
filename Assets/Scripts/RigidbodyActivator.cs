using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyActivator : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            other.GetComponent<MeshRenderer>().enabled = true;
            other.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (other.tag == "Prop")
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        //    other.GetComponent<MeshCollider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (other.tag == "Prop")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        //    other.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
