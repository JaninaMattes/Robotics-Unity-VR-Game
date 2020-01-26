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
    // controller Instanz
    protected Game_Manager controller = Game_Manager.Instance;


    // Start is called before the first frame update
    void Start()
    {
    maxobjectNumber = 60;
    //spawnedObjects = 0;

    minPositionX = -20;
    maxPositionX = 20;

    minPositionY = 20;
    maxPositionY = 40;

    minPositionZ = -20;
    maxPositionZ = 20;

    RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void RandomSpawn()
    {
        Renderer[] spawns = new Renderer[spawnees.Length];
        int i = 0;

        for (spawnedObjects = 0; spawnedObjects < maxobjectNumber; spawnedObjects++) {

            SpawnPosition.x = Random.Range(minPositionX, maxPositionX);
            SpawnPosition.y = Random.Range(minPositionY, maxPositionY);
            SpawnPosition.z = Random.Range(minPositionZ, maxPositionZ);

            randomInt = Random.Range(0,spawnees.Length);
            Instantiate(spawnees[randomInt], SpawnPosition, Quaternion.identity);

        }
        // Update all materials after new objects are spawned
        controller.GetMeshRenderer();        
        foreach (GameObject obj in spawnees)
        {
            obj.name = (string) obj.name + i;
            spawns[i] = obj.GetComponent<Renderer>();
            i++;
        }            
        controller.SetMaterials(spawns);
        controller.UpdateMaterial(controller.GetSnappedPatrone());
    }
}
