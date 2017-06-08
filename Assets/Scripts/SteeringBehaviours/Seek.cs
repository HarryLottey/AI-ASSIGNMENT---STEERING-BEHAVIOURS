using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 5f;
    
    public Vector3 force;

    public override Vector3 GetForce()
    {
        // SET force to zero
        Vector3 force = Vector3.zero;
        Vector3 desiredForce;
        // IF target is null
        if (target == null)
        {
            // RETURN force
            return force;
            
        }

        // SET desiredForce to direction from target to owner's position
        desiredForce = target.position - owner.transform.position;

        // SET desiredForce y to zero 
        desiredForce.y = 0f;

        // IF desiredForce is greater than stoppingDistance
        if(desiredForce.magnitude > stoppingDistance)
        {
            // SET desiredForce is greater than stopping Distance
                // SET desiredForce to desiredForce normalised * weighting
                // SET force to desiredForce - owner's velocity
            desiredForce = desiredForce.normalized * weighting;
            force = desiredForce - owner.velocity;
        }

        // RETURN force
        return force;
    }
            
}
