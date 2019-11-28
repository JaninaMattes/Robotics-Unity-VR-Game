using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSpawner : MonoBehaviour
{
    //Cube prefab
    public GameObject bumper;
    public GameObject player;
    private float timer;

    void onCollisionEnter(Collision col)
    {
        Vector3 contact = col.contacts[0].point;
        Instantiate(player, contact, Quaternion.identity);
    }
}
