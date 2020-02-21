using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ScoreKitchenBox : MonoBehaviour
{
    ScoringSystem scoringSystem;

    public GameObject score10;

    public GameObject score25;

    public GameObject scorePlus;

    public GameObject scoreMinus;

    GameObject activeCameraRig;

    public enum Relation
    {
        Plus = 0,
        Minus = 1,
    }

    public Relation PlusMinus;

    public enum ScoreValue
    {
        _10 = 10,
        _25 = 25,
    }

    public ScoreValue scoreValue;


    // Start is called before the first frame update
    void Start()
    {
        scoringSystem = FindObjectOfType<ScoringSystem>();

        Invoke("GetCameraRig", 0.05f);
    }

    void GetCameraRig()
    {
        activeCameraRig = GameObject.Find("CenterEyeAnchor");
    }

    public void ObjectInBox(bool useScoring)
    {
        if (PlusMinus == Relation.Minus)
        {
            if (useScoring)
            {
                scoringSystem.SubtractLocalScore((sbyte)scoreValue);

                GameObject scoreText = null;

                switch (scoreValue)
                {
                    case ScoreValue._10:
                        //Spawn Text 10_Cube
                        scoreText = Instantiate(score10, gameObject.transform.position, gameObject.transform.rotation);
                        break;
                    case ScoreValue._25:
                        //Spawn Text 25_Cube
                        scoreText = Instantiate(score25, gameObject.transform.position, gameObject.transform.rotation);
                        break;
                }

                GameObject vorzeichen = Instantiate(scoreMinus, gameObject.transform.position + new Vector3(1.5f, 0, 0), gameObject.transform.rotation);

                vorzeichen.transform.parent = scoreText.transform;

                //Vector3 dir = scoreText.transform.position - activeCameraRig.transform.position;
                scoreText.transform.LookAt(activeCameraRig.transform);

            }
        }
        else
        {


            if (useScoring)
            {
                scoringSystem.AddLocalScore((sbyte)scoreValue);
                GameObject scoreText = null;

                switch (scoreValue)
                {
                    case ScoreValue._10:
                        //Spawn Text 10_Cube
                        scoreText = Instantiate(score10, gameObject.transform.position, gameObject.transform.rotation);
                        break;
                    case ScoreValue._25:
                        //Spawn Text 25_Cube
                        scoreText = Instantiate(score25, gameObject.transform.position, gameObject.transform.rotation);
                        break;
                }

                GameObject vorzeichen = Instantiate(scorePlus, gameObject.transform.position + new Vector3(1.5f, 0, 0), gameObject.transform.rotation);

                vorzeichen.transform.parent = scoreText.transform;

                scoreText.transform.LookAt(activeCameraRig.transform);
            }
        }

        Destroy(gameObject);
    }
}
