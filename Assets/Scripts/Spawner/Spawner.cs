using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // Object we want to spawn
    public float spawnRate = 1f; // Spawn an objec tevery spawn rate interval (in seconds)
    [HideInInspector] public List<GameObject> objects = new List<GameObject>();

    public Vector3 randomPoint;

    private float spawnTimer = 0f; // Counts up every frame in seconds as our timer

    void OnDrawGizmos()
    {
        // Draw a cube to indicate where the box is that 
        // we're spawning objects
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
    
    // Generates a random point within the transform's scale
    Vector3 GenerteRandomPoint()
    {
        // SET halfScale to half of the transform's scale
        Vector3 halfScale = transform.localScale / 2; // * 0.5f
        // SET randomPoint vector to zero
        randomPoint = Vector3.zero;
        // SET randomPoint x,y,z to random range between
        // -halfScale to halfScale (HINT: can do individually)
        randomPoint.x = Random.Range(-halfScale.x, halfScale.x);
        randomPoint.y = 0;
        randomPoint.z = Random.Range(-halfScale.z, halfScale.z);
        // RETURN randomPoint
        return randomPoint;
    }

    // Spawns the prefab at a given position and with rotation
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        // SET clone to new instance of prefab
        GameObject clone = Instantiate(prefab);
        // ADD clone to objects list
        objects.Add(clone);
        // SET clone's posiiton to position
        clone.transform.position = position;
        // SET clone's rotation to rotation
        clone.transform.rotation = rotation;
    }


    // Update is called once per frame
    void Update()
    {
        // SET spawnTimer to spawnTimer + delta time
        spawnTimer = spawnTimer + Time.deltaTime;
        // IF spawnTimer > spawnRate
        if(spawnTimer > spawnRate)
        {
            // SET spawner position + randomPoint to GenerateRandomPoint()
            randomPoint = GenerteRandomPoint();
            // CALL Spawn() and pass GenerateRandomPoint(), Quarternion identity
            Spawn(transform.position + randomPoint,Quaternion.identity);
            // SET spawnTimer to zero
            spawnTimer = 0f;
        }

    }
}
