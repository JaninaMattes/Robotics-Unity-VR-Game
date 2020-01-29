using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlyingObjects : MonoBehaviour
{
    public GameObject[] spawnees;
    public GameObject[] spawnedObjectsArray;

    int randomInt;

    Game_Manager gameManager = Game_Manager.Instance;

    public List<string> excludeObj;

    public int maxobjectNumber = 60;
    public int spawnedObjects;

    public int minPositionX = -20;
    public int maxPositionX = 20;

    public int minPositionY = 20;
    public int maxPositionY = 40;

    public int minPositionZ = -20;
    public int maxPositionZ = 20;

    public Vector3 SpawnPosition;

    public Renderer[] spawnRenderer;

    public Renderer[] getRendererArray;


    // Start is called before the first frame update
    void Awake()
    {
        SpawnObjects();
    }
    void Start()
    {
        StartCoroutine(DisableEnableObjects(false, 0.05f));
    }

    public IEnumerator DisableEnableObjects(bool active, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (GameObject spawnedObject in spawnedObjectsArray)
        {
            spawnedObject.SetActive(active);
        }
    }

    public void SpawnObjects()
    {
        spawnedObjectsArray = new GameObject[maxobjectNumber];
        //spawnedObjects = 0;
        RandomSpawn();
    }

  

    void RandomSpawn()
    {
        Debug.Log("RandomSpawn");
        spawnRenderer = new Renderer[maxobjectNumber];
   

        for (spawnedObjects = 0; spawnedObjects < maxobjectNumber; spawnedObjects++) {

            SpawnPosition.x = Random.Range(minPositionX, maxPositionX);
            SpawnPosition.y = Random.Range(minPositionY, maxPositionY);
            SpawnPosition.z = Random.Range(minPositionZ, maxPositionZ);

            randomInt = Random.Range(0,spawnees.Length);
            spawnedObjectsArray[spawnedObjects]  =  Instantiate(spawnees[randomInt], SpawnPosition, Quaternion.identity);
            spawnRenderer[spawnedObjects] = spawnedObjectsArray[spawnedObjects].GetComponent<Renderer>();
        }

        
    }



    public void DestroyAllSpawness()
    {
        foreach (GameObject spawnee in spawnedObjectsArray)
        {
            if (spawnee != null)
            {
                spawnee.GetComponent<EnemyHit>().Hit(false);
            }
        }
    }
}
