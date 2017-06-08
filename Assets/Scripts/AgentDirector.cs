using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class AgentDirector : MonoBehaviour
{
    public Transform selectedTarget;
    public float rayDistance = 1000f;
    public LayerMask selectionLayer;
    private AIAgent[] agents;

    // Use this for initialization
    void Start()
    {
        // SET agents to FindObjectsOfType AIAgnet
        agents = FindObjectsOfType<AIAgent>();
    }

    // Apply selected target to all agents
    void ApplySelection()
    {
        
        // SET agents to FindObjectsOfType AIAgnet EVERY time we select a target
        agents = FindObjectsOfType<AIAgent>();
        // FOREACH agent in agents
        
        foreach (AIAgent agent in agents)
        {
            AIAgent selectedAgent = selectedTarget.GetComponent<AIAgent>();
            PursuitandEvade pursuitAndEvade = agent.GetComponent<PursuitandEvade>();
            
            // Check if the selected target is an AIAgent AND the current agent has PursuitandEvade attached
            if (selectedAgent != null && pursuitAndEvade != null)
            {
                pursuitAndEvade.agent = selectedAgent;
            }
            
            // SET pathFollowing to agent.GetComponent<PathFollowing>();
            PathFollowing pathfollowing = agent.GetComponent<PathFollowing>();
            // IF pathFollowing is not null
            if (pathfollowing != null)
            {
                // SET pathfollowing.target to selectedTarget
                pathfollowing.target = selectedTarget;
                // CALL pathFollowing.UpdatePath()
                pathfollowing.UpdatePath();
            }

        }
    }

    // Constantly checking for input
    void CheckSelection()
    {
        // SET ray to ray from Camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // SET hit to new RaycastHit
        RaycastHit hit = new RaycastHit();
        // IF Raycast() and pass ray, out hit, rayDistance, selectionLayer
        if (Physics.Raycast(ray, out hit, rayDistance, selectionLayer))
        {
            // CALL GizmosGL.addSphere() and pass hit.point , f, identity, color
            GizmosGL.AddSphere(hit.point, 5f, Quaternion.identity, Color.red);
            // IF user clicked left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // SET selectedTarget to hit.collider.transform
                selectedTarget = hit.collider.transform;
                // CALL applySelection
                ApplySelection();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Call CheckSelection
        CheckSelection();
    }
}
