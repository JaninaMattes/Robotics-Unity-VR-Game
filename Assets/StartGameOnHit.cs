using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameOnHit : MonoBehaviour
{
    public ScoringSystem scoringSystem;

    private void Start()
    {
        gameObject.tag = "StartButton";
    }

    public void StartGameAndScore()
    {
        scoringSystem.startGame();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>(),true);
        }
    }
}
