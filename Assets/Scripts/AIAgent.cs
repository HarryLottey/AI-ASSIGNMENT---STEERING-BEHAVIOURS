using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Vector3 force;
    public Vector3 velocity;
    public float maxVelocity;
    
    

    private SteeringBehaviour[] behaviors;
    
    void Start()
    {
        // Seek classifies as a steering behaviour because it inherits from it
        behaviors = GetComponents<SteeringBehaviour>();
    }

    void Update()
    {
        ComputeForces();
        ApplyVelocity();

    }

    void ComputeForces()
    {
        // SET force to zero
        force = Vector3.zero; // maybe change this 
        // FOR i = 0 to behaviours count
        for (int i = 0; i < behaviors.Length; i++)
        {
            SteeringBehaviour behavior = behaviors[i];

            // IF behaviour is NOT enabled (script.notenabled)
            if (!behavior.enabled)
            {
                continue;
            }
            //CONTINUE

            // SET force to force + behaviour's force
            force = force + behavior.GetForce();
            // IF force is greater than maxVelocity
            if (force.magnitude > maxVelocity)
            {
                // SET force to force normalized x maxVelocity
                force = force.normalized * maxVelocity;
                // BREAK
                break;
            }

        }


    }

    void ApplyVelocity()
    {
        // SET velocity to velocity + force * delta time
        velocity = velocity + force * Time.deltaTime;
        // IF velocity is greater than maxVelocity
        if(velocity.magnitude > maxVelocity)
        {
            // SET velocity to velocity normalized x maxVelocity
            velocity = velocity.normalized * maxVelocity;
        }

        // IF velocity is greater than zero
        if (velocity != Vector3.zero) // slower alternative: velocity.magnitude > 0
        {
            // SET position to position + velocity x delta time
            transform.position = transform.position + velocity * Time.deltaTime;
            // SET rotation to Quarternion.LookRotation velocity
            transform.rotation = Quaternion.LookRotation(velocity);
        }

    }
}
