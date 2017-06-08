using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitandEvade : SteeringBehaviour
{
    public AIAgent agent; // target
    public float stoppingDistance = 1f;
    public float maxSpeed = 5f;
    public float T; 
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        FuturePosition();
    }

    void FuturePosition()
    {
        transform.position = transform.position + owner.velocity * T;
    }

    public Vector3 Pursuit(AIAgent agent) // t = Rigid body, but we need to return vector 3?
    {

        // T = distance between target and pursuer divided by max velocity
        Vector3 distance = transform.position - agent.transform.position;
        T = distance.magnitude / agent.maxVelocity;
        Vector3 futurePosition = agent.transform.position + agent.velocity * T;
        return Seek(futurePosition); // make seek in this script
    }

    public Vector3 Evade(AIAgent agent)
    {
        Vector3 distance = transform.position - agent.transform.position;
        float updatesAhead = distance.magnitude / agent.maxVelocity;
        Vector3 futurePosition = agent.transform.position + agent.velocity * updatesAhead;
        return Flee(futurePosition);
    }

    Vector3 Flee(Vector3 target)
    {
        // THIS IS SEEK, REVERSE THIS SO ITS FLEE?

        Vector3 force = Vector3.zero;
        // SET desiredForce to target - transform's position
        Vector3 desiredForce = transform.position -  target;
        // SET desiredForce.y to zero
        desiredForce.y = 0;

        // IF desiredForce's length is greater than distance
        if (stoppingDistance > desiredForce.magnitude)
        {
            // SET desiredForce to desiredForce.normalized * weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce - owner's velocity
            force = owner.velocity - desiredForce;

            return force;
        }

        return target;
    }
    
    Vector3 Truncate(Vector3 force, float max)
    {
        if(force.magnitude > max)
        {
            force = force.normalized * max;
        }
        return force;
    }

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;
        force = Pursuit(agent);
        force = Truncate(force, maxSpeed);

        // SET force to 

        return force;
    }
    Vector3 Seek(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        // SET desiredForce to target - transform's position
        Vector3 desiredForce = target - transform.position;
        // SET desiredForce.y to zero
        desiredForce.y = 0;
        
        // IF desiredForce's length is greater than distance
        if (desiredForce.magnitude > stoppingDistance)
        {
            // SET desiredForce to desiredForce.normalized * weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce - owner's velocity
            force = desiredForce - owner.velocity;

            return force;
        }
        
        return target;

    }

}