using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public int localScore = 0;
    public Text scoreAnzeige;

    public Text countdownAnzeige;

    public float maxTimeToComplete = 180;
    float remainingTime;

    private void Start()
    {
        UpdateScoreAnzeige();
        remainingTime = maxTimeToComplete;
        //InvokeRepeating("Countdown",0f, 0.01f);
        StartCoroutine(Countdown());
    }

    void UpdateScoreAnzeige()
    {
        scoreAnzeige.text = GetLocalScore().ToString() + " Pkt";
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

    IEnumerator Countdown()
    {
        while (remainingTime >= 0)
        {
            remainingTime -= Time.deltaTime;

            float minutes = Mathf.Floor(remainingTime / 60);
            float seconds = Mathf.RoundToInt(remainingTime % 60);
            string minText = minutes.ToString();
            string secText = Mathf.RoundToInt(seconds).ToString();

            if (minutes < 10)
            {
                minText = "0" + minutes.ToString();
            }
            if (seconds < 10)
            {
                secText = "0" + Mathf.RoundToInt(seconds).ToString();
            }

            countdownAnzeige.text = minText + ":" + secText;
            yield return new WaitForEndOfFrame();
        }
            
    }



   
}
