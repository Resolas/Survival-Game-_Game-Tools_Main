using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackDroneAI : MonoBehaviour
{
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = Vector3.zero;

        targetObject = GameObject.Find("Player").GetComponent<Transform>();
    }

    public Transform targetObject;
    private Vector3 destination;
    private NavMeshAgent agent;
    public float range = 1000f;


    [Header("Weapon Settings")]

    public GameObject bullet;
    public float fireRate = 1f;
    public Transform[] firePoints;
    private float shootBullet = 0;
    public bool isVisible;
    public float accuracy = 3f;

    private void Update()
    {

        Vector3 distance = targetObject.position - transform.position;
        float distMg = distance.sqrMagnitude / 1000;
        //  Debug.Log(distMg);
        if (targetObject && distMg < range)
        {
            isTargetVisiblePys();
            
        }
        else
        {
            //  Debug.Log("WANDER");
          //  RandomNavSphere(transform.position, range, 8);

        }
        shootBullet += 1 * Time.deltaTime;


        if (isVisible && shootBullet > 1 / fireRate)
        {
            ShootLaser();
            shootBullet = 0;
        }
    }

    public void isTargetVisiblePys()
    {


        

        var direction = targetObject.position - transform.position;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity) && hit.collider.tag == "Player")
        {
            isVisible = true;
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
            //   Debug.Log("Is Visible");

            agent.SetDestination(targetObject.position);
        }
        else
        {
            isVisible = false;
            Debug.DrawRay(transform.position, direction * 1000, Color.white);
            //    Debug.Log("Is NOT Visible");
        }

    }

    public void ShootLaser()
    {

        for (int i = 0; i < firePoints.Length; i++)
        {
            Debug.Log("SHOOT");
            firePoints[i].transform.LookAt(targetObject);
            firePoints[i].transform.Rotate(Random.Range(-accuracy,accuracy), Random.Range(-accuracy,accuracy), Random.Range(-accuracy,accuracy));
            Instantiate(bullet,firePoints[i].transform.position,firePoints[i].transform.rotation);

        }


    }
}
