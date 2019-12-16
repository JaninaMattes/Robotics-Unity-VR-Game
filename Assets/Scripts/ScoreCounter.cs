using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private static int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        updateScoreView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: 

    /// <summary>
    /// Update the score on text widget in GUI
    /// </summary>
    public void updateScoreView()
    {
    }
}
