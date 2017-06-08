using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollowing : SteeringBehaviour
{
    // Combine These 3 Behaviours to acomplish this

    // Arrive
    public Transform target;
    public Vector3 max_Velocity;

    

    // Evade
    // Seperation


    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        Vector3 desiredVelocity;
        Vector3 steering;
        if(target == null)
        {
            return force;
        }

        desiredVelocity = target.position - owner.transform.position;

        //desiredVelocity = desiredVelocity.Normalize(target.position - owner.transform.position);
        steering = desiredVelocity - owner.velocity;

        return force;
    }

    


    void Start()
    {

    }

    
    void Update()
    {

    }
}
