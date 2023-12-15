using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent agent; // Asks for an object interacting with the navmesh

    public float squareOfMovement = 100f;
    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    private float xPosition;
    private float yPosition;
    private float zPosition;
    public float closeEnough = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // Setting paramaters for enemy movement
        xMin = -squareOfMovement;
        xMax = squareOfMovement;
        zMin = -squareOfMovement;
        zMax = squareOfMovement;

        newLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, new Vector3(xPosition, yPosition, zPosition)) <= closeEnough) // If location to player is not enough to meet threshold to chase
        {
            newLocation(); 
        }
        
    }

    public void newLocation()
    {
        yPosition = transform.position.y;
        xPosition = Random.Range(xMin, xMax); // Generates a new random X position within paramaters
        zPosition = Random.Range(zMin, zMax); // Generates new random Y position within paramaters

        agent.SetDestination(new Vector3(xPosition, yPosition, zPosition)); // Sets the new position for the enemy to move to with previously generated positions
    }
}
