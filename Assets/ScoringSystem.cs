using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    public int localScore = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void UpdateScoreAnzeige()
    {
        Debug.Log(GetLocalScore());
    }

    public void AddLocalScore(int points)
    {
        localScore += points;
        UpdateScoreAnzeige();
    }

    public void SubtractLocalScore(int points)
    {
        localScore -= points;
        UpdateScoreAnzeige();
    }

    public int GetLocalScore()
    {
        return localScore;
    }



   
}
