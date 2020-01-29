using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameOnHit : MonoBehaviour
{
    public ScoringSystem scoringSystem;
    public GameObject startText;
    bool started;

    private void Start()
    {
        gameObject.tag = "StartButton";
    }

    public void StartGameAndScore()
    {
        if (!started)
        {
            scoringSystem.startGame();
            startText.SetActive(false);
            started = true;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Target")
    //    {
    //        Debug.Log("Eigener Collider: " + gameObject.GetComponent<Collider>().tag);
    //        Debug.Log("Fremder Collider: " + collision.collider.tag);
    //        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),collision.collider,true);
    //    }
    //}
}
