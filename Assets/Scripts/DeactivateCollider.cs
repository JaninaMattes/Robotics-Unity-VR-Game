using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeactivateCollider : MonoBehaviour
{
    public Collider controllerLeft;
    public Collider controllerRight;
    public int sceneCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            controllerLeft.enabled = false;
            controllerRight.enabled = false;
        }
        if(SceneManager.GetSceneAt(1).isLoaded)
        {
            controllerLeft.enabled = true;
            controllerRight.enabled = true;
        }
    }
}
