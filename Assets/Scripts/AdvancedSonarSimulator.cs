using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///  Out of testing purpose for shader arrays.
///  
/// </summary>

public class AdvancedSonarSimulator : MonoBehaviour
{

    public float radius; // Radius for the ring
    public float shotCooldown; 
    private float timer;
    private List<Vector4> hits; // LinkedList for adding and removing are better to be used
    public LaserController controller = new LaserController();
    public float sonarLifeTime;

    // Use this for initialization
    void Start()
    {
        hits = new List<Vector4>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        while (timer > shotCooldown)
        {
            timer -= shotCooldown;
            // create new hits
            var point = Random.onUnitSphere * radius;
            hits.Add(new Vector4(point.x, point.y, point.z, 0));
        }
        hits = hits
               .Select(hit => new Vector4(hit.x, hit.y, hit.z, hit.w + (Time.deltaTime / sonarLifeTime)))
               .Where(hit => hit.w <= 1).ToList(); // delete all invalid from list

        controller.points = hits.ToArray(); // hand the list over to shader controller
    }

}