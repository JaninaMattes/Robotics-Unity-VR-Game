using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnHit : MonoBehaviour
{
    public int targetSceneIndex = 0;
    public float delay = 0;

    private void Start()
    {
        gameObject.tag = "Target";
    }

    public void InvokeLoadScene()
    {
        Invoke("LoadScene", delay);
    }
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Target")
       {
           Debug.Log("Eigener Collider: " + gameObject.GetComponent<Collider>().tag);
           Debug.Log("Fremder Collider: " + collision.collider.tag);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(),collision.collider,true);
     }
    }


    void LoadScene()
    {
        SceneManager.LoadSceneAsync(targetSceneIndex);
    }
}
