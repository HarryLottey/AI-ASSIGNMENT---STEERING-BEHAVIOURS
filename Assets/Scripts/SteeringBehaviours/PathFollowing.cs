using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class PathFollowing : SteeringBehaviour
{

    public Transform target;
    public float nodeRadius = 5f; // Distance to current node
    public float targetRadius = 3f; // Distance to end node
    private Graph graph;
    private int currentNode;
    private bool isAtTarget = false;
    public List<Node> path;


    void Start()
    {
        // SET graph to FindObjectOfType Graph
        graph = FindObjectOfType<Graph>();
        // IF graph is null
        if (graph == null)
        {
            // CALL Debug.LogError() and pass an Error message
            Debug.LogError("A graph does not exist in the scene");
            // Call Debug.Break()
            Debug.Break();
        }

    }

    // Updats list of nodes for Path Following
    public void UpdatePath()
    {
        // SET path to graph.Findpath() and pass transform's position, target's posiiton
        path = graph.FindPath(transform.position, target.position);
        // SET currentNode to zero
        currentNode = 0;
    }

    #region SEEK
    // Special version of Seek that takes into account the node radius & target radius
    Vector3 Seek(Vector3 target)
    {
        Vector3 force = Vector3.zero;
        // SET desiredForce to target - transform's position
        Vector3 desiredForce = target - transform.position;
        // SET desiredForce.y to zero
        desiredForce.y = 0;
        // SET distance to zero
        float distance = isAtTarget ? targetRadius : nodeRadius;

        // IF desiredForce's length is greater than distance
        if (desiredForce.magnitude > distance)
        {
            // SET desiredForce to desiredForce.normalized * weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce - owner's velocity
            force = desiredForce - owner.velocity;
        }

        return force;
    }





    #endregion

    #region GETFORCE
    // Calculates force for behaviour
    public override Vector3 GetForce()
    {
        // SET force to zero
        Vector3 force = Vector3.zero;

        // IF path is not null AND path count is greater than zero
        if (path != null && path.Count > 0)
        {
            // SET currentPos to current path node's position
            Vector3 currentPos = path[currentNode].position;
            // IF Vector3.Distance(tranform's position, currentPos)
            if (Vector3.Distance(transform.position, currentPos) <= nodeRadius)
            {
                // Increment currentnode
                currentNode++;
                //IF currentNode is greater than or equal to path.Count
                if (currentNode >= path.Count)
                {
                    // SET currentNode to path.Count -1
                    currentNode = path.Count - 1;
                }
            }

            print("Calculating Seek");
            // SET force to Seek() and pass currentPos
            force = Seek(currentPos);

            print(force);

            #region GIZMOS
            // SET prevPosition to path[0].position
            Vector3 prevPosition = path[0].position; // nothing to get position of??
                                                     // FOREACH node in path
            foreach (Node node in path)
            {
                // CALL GizmosGL.AddSphere() and pass node's position, graph's nodeRadius, identity, any color
                GizmosGL.AddSphere(node.position, graph.nodeRadius, Quaternion.identity, Color.blue);
                // CALL GizmosGL.AddLine() and pass prev, node's position, 0.1f,0.1f,any color, any color
                GizmosGL.AddLine(prevPosition, node.position, 0.1f, 0.1f, Color.red, Color.yellow);
                // SET prev to node's position
                prevPosition = node.position;
            }

            #endregion
        }

        // RETURN Force
        return force;
    }
    #endregion

}  





