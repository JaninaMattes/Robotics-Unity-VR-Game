using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasergun : MonoBehaviour
{

    public GameObject laserBlastPrefab;

    public Transform laserGunOrigin;

    public int projectileSpeed = 200;

    public GameObject explosionPrefab;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject blast = Instantiate(laserBlastPrefab, laserGunOrigin.position, gameObject.transform.rotation);
        LaserProjectile projectileScript = blast.GetComponent<LaserProjectile>();
        projectileScript.speed = projectileSpeed;
        projectileScript.explosionPrefab = explosionPrefab;
    }
}
