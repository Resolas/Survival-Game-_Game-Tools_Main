using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<SphereCollider>();
    }

    private SphereCollider myCollider;
    public GameObject DebrisParticles;
  //  public float minBlastRadius = 1000f;
    public float maxBlastRadius = 2000f;
    public float time = 0.7f;


    // Update is called once per frame
    void FixedUpdate()
    {

        myCollider.radius = Mathf.Lerp(myCollider.radius,maxBlastRadius,Time.deltaTime);
        /*
        Vector3 explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos,myCollider.radius);

        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(10,explosionPos,myCollider.radius);
            }

        }

        */

    }
    
    private void OnTriggerStay(Collider other)
    {
        

        if (other.tag == "Structure")
        {
            Debug.Log("DESTROY");

            Instantiate(DebrisParticles,new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity);
             Destroy(other.gameObject);


         //   other.gameObject.GetComponent<Rigidbody>().WakeUp();
         //   other.gameObject.GetComponent<MeshCollider>().convex = true;
            

        }

        if (other.tag == "Player" || other.tag == "Enemy")
        {

            var direction = other.transform.position - transform.position;
            bool isVisible = false;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && hit.collider == other)
            {
                isVisible = true;
                Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                //   Debug.Log("Is Visible");

                
            }
            else
            {
                isVisible = false;
                Debug.DrawRay(transform.position, direction * 1000, Color.white);
                //    Debug.Log("Is NOT Visible");
            }




            if (other.tag == "Player" && isVisible)
            {
                PlayerStats getStats = other.GetComponent<PlayerStats>();
                getStats.isDead = true;
            }

            if (other.tag == "Enemy" && isVisible)
            {
                Destroy(other.gameObject);
            }

        }
        

        
    }
    /*
    private void OnCollisionStay(Collision collision)
    {

        if (collision.transform.tag == "Structure")
        {
            Debug.Log("DESTROY");
            Destroy(collision.gameObject);
        }
        Destroy(collision.gameObject);
    }
    */

}
