using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class StartLevel : MonoBehaviour
{
    // Select the correct level index for a scene
    public int LevelIndex;
    private Collision Col = null;
    private bool IsTouched = false;
    // Startposition in the levels needs to be everywhere the same
    private Vector3 locationPoint = new Vector3(0.0f, 0.0f, -6.0f);

    // Update is called once per frame
    void Update()
    {
        if (Col != null) // TODO: Erweitern um onTouch VRTK
        {
            SceneManager.LoadScene(LevelIndex);
            transform.position = locationPoint;
        }       
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene(LevelIndex);
    }

    void OnCollisionEnter(Collision collision)
    {
        Col = collision;
    }
}
