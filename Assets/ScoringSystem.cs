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

    public SpawnFlyingObjects spawnFlyingObjects;

    Game_Manager gameManager = Game_Manager.Instance;

    public AudioSource gamePlaySound;

    float remainingTime;

    bool scoreLocked;

    public Material[] sensorMaterials;

    public GameObject restartTarget;
    public GameObject quitTarget;

    private void Start()
    {
        UpdateScoreAnzeige();
        remainingTime = maxTimeToComplete;
        
        scoreLocked = true;
        restartTarget.SetActive(false);
        quitTarget.SetActive(false);

        gameManager.SetAllMaterials(sensorMaterials);

        
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
    }

    public void startGame()
    {
        gamePlaySound.Play();
        scoreLocked = false;
        StartCoroutine(Countdown());
        //spawnFlyingObjects.SpawnObjects();
        StartCoroutine(spawnFlyingObjects.DisableEnableObjects(true, 0f));
    }

    void UpdateScoreAnzeige()
    {
        scoreAnzeige.text = GetLocalScore().ToString() + " Pkt";
    }

    public void AddLocalScore(int points)
    {
        if (!scoreLocked)
        {
            localScore += points;
            UpdateScoreAnzeige();
        }
    }

    public void SubtractLocalScore(int points)
    {
        if (!scoreLocked)
        {
            localScore -= points;
            UpdateScoreAnzeige();
        }
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
        countdownAnzeige.text = "00:00";
        scoreLocked = true;
        restartTarget.SetActive(true);
        quitTarget.SetActive(true);
        gameManager.AddPlayerScore(localScore);
        spawnFlyingObjects.DestroyAllSpawness();
        gamePlaySound.Stop();
    }



   
}
