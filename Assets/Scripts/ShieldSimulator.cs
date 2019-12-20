using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///  Out of testing purpose for shader arrays.
///  
/// </summary>

public class ShieldSimulator : MonoBehaviour
{

    public float radius;
    public float shotCooldown;
    private float timer;
    private List<Vector4> projectiles;
    public SonarRingController controller = new SonarRingController();
    public float projectileLife;

    // Use this for initialization
    void Start()
    {
        projectiles = new List<Vector4>();
    }

    // Update is called once per frame
    void Update()
    {
        projectiles = projectiles
               .Select(projectile => new Vector4(projectile.x, projectile.y, projectile.z, projectile.w + (Time.deltaTime / projectileLife)))
               .Where(projectile => projectile.w <= 1).ToList();

        projectiles.ToArray().CopyTo(controller.points, 0);
    }

    void OnProjectileHit(Vector3 worldSpaceImpact)
    {
        Debug.Log(worldSpaceImpact);
        worldSpaceImpact = this.transform.worldToLocalMatrix * worldSpaceImpact;
        projectiles.Add(new Vector4(worldSpaceImpact.x, worldSpaceImpact.y, worldSpaceImpact.z, 0));
    }
}