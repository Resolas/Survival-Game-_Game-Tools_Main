﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class TESTAI : MonoBehaviour
{
    // Reference to NavMeshAgent component
    NavMeshAgent navAgent;
    Vector3 destination;

    // Canvas Array
    public Text[] canvasUpdate;

    // Decision Tree control booleans 
    public bool isVisible;
    public bool isAudible;
    public bool isClose;

    // Also to the game object we will follow
    public Transform targetObject;

    // Waypoints for Patrol functionality
    int nextIndex;
    public GameObject[] waypoints;

    // Sight variables
    public float fieldOfViewAngle = 360.0f;
    private SphereCollider col;


    void Start()
    {
        // Get the component reference
        navAgent = GetComponent<NavMeshAgent>();

        // Patrol Scene
        destination = NextWaypoint(Vector3.zero);

        col = GetComponent<SphereCollider>();
    }

    // This is where we can create our Decision Tree
    void FixedUpdate()
    {
        // Check if target object exists
        // If so, runn Perception Tests
        if (targetObject)
        {
            // Check is the Player is Visible
            isPlayerVisible();
            // Check is the Player is Audible
            isPlayerAudible();
            // Check is the Player is Visible
            isPlayerClose();


            // Check for visibility and proximity
            if (isVisible && isClose)
            {
                // If Randomball is visible and is close, then SEEK
                Debug.Log("SEEK");
                seekFunction();
            }
            else if (isVisible && !isClose)
            {
                // If Randomball is visible and not close, then PATROL
                patrolFunction();
            }
            else if (!isVisible && !isAudible)
            {
                // If Randomball is not visible and not audable, then IDLE
                IdleFunction();
            }
            else if (!isVisible && isAudible)
            {
                // If Randomball is visible and not close, then PATROL
                patrolFunction();
            }
        }
        else
        {
            // If RandomBall has been destroyed then Idle
            IdleFunction();
        }
        navAgent.SetDestination(destination);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "RandomBall")
        {
            // Stop the chase by destroying the yellow ball
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name == "Cube")
        {
            // Change the colour of the object
            col.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    // Seek out RandomBall
    void seekFunction()
    {
        // Seek out the Enemy
        destination = targetObject.position;
        // Update Canvas with current state
        canvasUpdate[3].text = "Seeking";
    }
    // Patrol Array of cubes (added to script)
    void patrolFunction()
    {
        // If within 2.5, then move onto next waypoint in array
        if (Vector3.Distance(transform.position, destination) < 2.5)
        {
            destination = NextWaypoint(destination);
        }
        // Update Canvas with current state
        canvasUpdate[3].text = "Patrolling";
    }
    // Idle at (0,0,0)
    void IdleFunction()
    {
        // Idle at 0
        destination = new Vector3(0, 0, 0);
        // Update Canvas with current state
        canvasUpdate[3].text = "Idling";
    }

    // Function that loops through waypoints for the Patrol fucntionality
    public Vector3 NextWaypoint(Vector3 currentPosition)
    {
        Debug.Log(currentPosition);
        if (currentPosition != Vector3.zero)
        {
            // Find array index of given waypoint
            for (int i = 0; i < waypoints.Length; i++)
            {
                // Once found calculate next one
                if (currentPosition == waypoints[i].transform.position)
                {
                    // Modulus operator helps to avoid to go out of bounds
                    // And resets to 0 the index count once we reach the end of the array
                    nextIndex = (i + 1) % waypoints.Length;
                }
            }
        }
        else
        {
            // Default is first index in array 
            nextIndex = 0;
        }
        return waypoints[nextIndex].transform.position;
    }

    // Checks if Player is visible using Raycast
    public void isPlayerVisible()
    {

        // Create a vector from the enemy to the player and store the angle between it and forward.
        Vector3 direction = targetObject.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        Debug.Log("ANGLE " + angle);
        // Create NavMesh hit var
        NavMeshHit hit;

        // If the Ray cast hits something other than the target, then true is returned, if not false
        // So !hit is used to specify visibility and...
        // If the angle between forward and where the player is, is less than half the angle of view...
        if (!navAgent.Raycast(targetObject.transform.position, out hit) && angle < fieldOfViewAngle * 0.5f)
        {
            // ... the player is Visible
            Debug.DrawLine(transform.position, targetObject.transform.position, Color.red);
            Debug.Log("Player is VISIBLE");
            isVisible = true;
            // Update Close Text on Canvas
            canvasUpdate[0].enabled = true;

        }
        else
        {
            // ... the player is Not Visible
            isVisible = false;
            Debug.Log("Player is NOT VISIBLE");
            // Update Close Text on Canvas
            canvasUpdate[0].enabled = false;
        }
    }

    // Checks if player is Audible using simple distance calculation
    public void isPlayerAudible()
    {
        // If direct distance < 20, then audible
        if (Vector3.Distance(transform.position, targetObject.position) < 20.0f)
        {
            // Is Audible
            isAudible = true;
            // Update Close Text on Canvas
            canvasUpdate[1].enabled = true;
        }
        else
        {
            // Is not Audible
            isAudible = false;
            // Update Close Text on Canvas
            canvasUpdate[1].enabled = false;
        }
    }

    // Check is Player is CloseBy based on the NavMesh
    // Distance to Player via the NavMesh is calculated here
    // If Distance < 30, then isClose == true
    public void isPlayerClose()
    {

        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (navAgent.enabled)
            navAgent.CalculatePath(targetObject.position, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetObject.position;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        if (pathLength < 20.0f)
        {

            Debug.Log("Path Length: " + pathLength);

            // Set Close Bool true
            isClose = true;
            // Update Close Text on Canvas
            canvasUpdate[2].enabled = true;
        }
        else
        {

            // Set Close Bool false
            isClose = false;
            // Update Close Text on Canvas
            canvasUpdate[2].enabled = false;
        }


    }

}
