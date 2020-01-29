using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject particleSystem;

    public enum Relation
    {
        Friend = 0,
        Enemy = 1,
    }

    public Relation relation;

    

    public float scoreValue = 10;


    public void Hit()
    {
        ParticleSystem ps = Instantiate(particleSystem, gameObject.transform.position, gameObject.transform.rotation).GetComponent<ParticleSystem>();
        var psMain = ps.main;
        if (relation == Relation.Enemy)
        {
            psMain.startColor = new Color(0.5f,0,0,1);
        }
        else
        {

        }

        Destroy(gameObject);
    }

}
