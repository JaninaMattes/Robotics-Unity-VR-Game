using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    // Select the correct level index for a scene
    public int LevelIndex;
    private Collision col = null;
    private bool touched = false;
    
    // Update is called once per frame
    void Update()
    {
        if (col != null) // TODO: Erweitern um onTouch VRTK
        {
            SceneManager.LoadScene(LevelIndex);
        }       
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene(LevelIndex);
    }

    void OnCollisionEnter(Collision col)
    {
        col = col;
    }
}
