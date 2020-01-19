using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlyingObjects : MonoBehaviour
{
    public GameObject[] spawnees;

    int randomInt;

    public int maxobjectNumber;
    public int spawnedObjects;

    public int minPositionX;
    public int maxPositionX;

    public int minPositionY;
    public int maxPositionY;

    public int minPositionZ;
    public int maxPositionZ;

    public Vector3 SpawnPosition;


    // Start is called before the first frame update
    void Start()
    {
    maxobjectNumber = 20;
    //spawnedObjects = 0;

    minPositionX = -40;
    maxPositionX = 40;

    minPositionY = 20;
    maxPositionY = 40;

    minPositionZ = -40;
    maxPositionZ = 40;

}

    // Update is called once per frame
    void Update()
    {
        SpawnPosition.x = Random.Range(minPositionX, maxPositionX);
        SpawnPosition.y = Random.Range(minPositionY, maxPositionY);
        SpawnPosition.z = Random.Range(minPositionZ, maxPositionZ);

        RandomSpawn();
    }

    void RandomSpawn()
    {
        for (spawnedObjects = 0; spawnedObjects <= maxobjectNumber; spawnedObjects++) {

        randomInt = Random.Range(0,spawnees.Length);
        Instantiate(spawnees[randomInt], SpawnPosition, Quaternion.identity);

        }
    }
}
