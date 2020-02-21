using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOnHit : MonoBehaviour
{
    public ScoringSystem scoringSystem;
    public GameObject startText;
    public GameObject ChecklistBox;
    public bool started;

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

            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                ChecklistBox.SetActive(true);
            }
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
