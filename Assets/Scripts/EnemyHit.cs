using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject particleSystem;

    public ScoringSystem scoringSystem;


    public enum Relation
    {
        Friend = 0,
        Enemy = 1,
    }

    public Relation relation;

    

    public int scoreValue = 10;


    private void Start()
    {
        scoringSystem = FindObjectOfType<ScoringSystem>();
    }


    public void Hit()
    {
        ParticleSystem ps = Instantiate(particleSystem, gameObject.transform.position, gameObject.transform.rotation).GetComponent<ParticleSystem>();
        var psMain = ps.main;
        if (relation == Relation.Enemy)
        {
            psMain.startColor = new Color(0.5f,0,0,1);
            scoringSystem.SubtractLocalScore(scoreValue);
        }
        else
        {
            scoringSystem.AddLocalScore(scoreValue);
        }

        Destroy(gameObject);
    }

}
