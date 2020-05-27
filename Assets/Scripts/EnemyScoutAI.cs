using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScoutAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = Vector3.zero;

        targetObject = GameObject.Find("Player").GetComponent<Transform>();
    }

    public float fieldOfViewAngle = 360f;
    public bool isVisible = false;

    private NavMeshAgent agent;
    private Vector3 destination;
    public float range = 1000f;
    public float launchTimer = 10f;
    public float maxTimer = 10f;

    public Transform targetObject;
    

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
            RandomNavSphere(transform.position,range,8);

        }

        LaunchNuke();

    }



    public void isTargetVisible()
    {
        /*
        Vector3 direction = targetObject.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        NavMeshHit hit;

        if (!agent.Raycast(targetObject.transform.position, out hit) && angle < fieldOfViewAngle * 0.5f)
        {

            isVisible = true;
            Debug.Log("Player is Visible");
            Debug.DrawLine(transform.position,targetObject.position, Color.red);
        }
        else
        {
            isVisible = false;
            Debug.Log("Player is not Visible");
        }
        */

    }

    public void isTargetVisiblePys()
    {

    
        // Bit shift the index of the layer (8) to get a bit mask
      //  int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
      //  layerMask = ~layerMask;

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

    public void LaunchNuke()
    {

        Vector3 distance = targetObject.position - transform.position;
        float distMg = distance.sqrMagnitude / 1000;

        if (isVisible == true && distMg < range && PlayerStats.nuke != true)        // If nuke is not launched and within half range and in LOS, countdown
        {
            if (launchTimer >= 0)
            {
                launchTimer -= 1 * Time.deltaTime;
            }


            if (launchTimer <= 0)
            {


                PlayerStats.targetScout = gameObject;  // sets the target position for nuke to spawn on top of
                PlayerStats.nuke = true;

            }

        }
        else
        {

            if (launchTimer <= maxTimer)
            {
                launchTimer += 1 * Time.deltaTime;
            }

        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randomPos = Random.insideUnitSphere * dist;
        randomPos += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPos, out navHit, dist, layerMask);
        return navHit.position;

    }

    public void Wander()
    {

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
