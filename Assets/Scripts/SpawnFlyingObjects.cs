using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlyingObjects : MonoBehaviour
{
    public GameObject[] spawnees;

    int randomInt;

    public int maxobjectNumber = 60;
    public int spawnedObjects;

    public int minPositionX = -20;
    public int maxPositionX = 20;

    public int minPositionY = 20;
    public int maxPositionY = 40;

    public int minPositionZ = -20;
    public int maxPositionZ = 20;

    public Vector3 SpawnPosition;


    // Start is called before the first frame update
    void Awake()
    {
    
    //spawnedObjects = 0;
    RandomSpawn();
    }

  

    void RandomSpawn()
    {
        
        for (spawnedObjects = 0; spawnedObjects < maxobjectNumber; spawnedObjects++) {

            SpawnPosition.x = Random.Range(minPositionX, maxPositionX);
            SpawnPosition.y = Random.Range(minPositionY, maxPositionY);
            SpawnPosition.z = Random.Range(minPositionZ, maxPositionZ);

            randomInt = Random.Range(0,spawnees.Length);
            Instantiate(spawnees[randomInt], SpawnPosition, Quaternion.identity);

        }
    }
}
