using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnHit : MonoBehaviour
{
    public int targetSceneIndex = 0;
    public float delay = 0;

    public void Start()
    {
        gameObject.tag = "Target";
    }

    public void InvokeLoadScene()
    {
        Invoke("LoadScene", delay);
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(targetSceneIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            InvokeLoadScene();
        }
    }
}
