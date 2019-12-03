using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BumperSpawner : MonoBehaviour
{
    public GameObject bumperClone;
    public float distance;
    private GameObject[] activeBumpers;
    public float fadeStart;
    public float fadeEnd;
    public float fadeSpeed;
    public float durationTime;
    public float rimPowerT;

    void Update()
    {
        activeBumpers = GameObject.FindGameObjectsWithTag("Bumper");

        if (SceneManager.GetSceneAt(0).isLoaded)
        {
            this.GetComponent<Collider>().enabled = false;
        }
        else
        {
            this.GetComponent<Collider>().enabled = true;
        }

    }
    /// <summary>
    /// On Collision Enter a new bumper object will be 
    /// instantiated and attached to the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        Vector3 contact = col.contacts[0].point;
        if (SceneManager.GetSceneAt(1).isLoaded)
        {
            if (activeBumpers.Length < 1)
            {
                Instantiate(bumperClone, new Vector3(contact.x, Camera.main.transform.position.y - 0.7f, contact.z) + 
                    Camera.main.transform.forward * distance, Quaternion.identity);
            }
        }
    }

    IEnumerator FadeBumper(float duration)
    {
        Material bumperM = bumperClone.GetComponent<Renderer>().material;
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime * fadeSpeed;
            float rimPower = Mathf.Lerp(fadeStart, fadeEnd, counter / duration);
            activeBumpers[0].GetComponent<Renderer>().material.SetFloat("_RimPower", rimPower);
            yield return null;
        }
        if (counter == duration)
        {
            for (int i = 0; i < activeBumpers.Length; i++)
            {
                Destroy(activeBumpers[i]);
            }
        }
    }

    void DestroyBumpers()
    {
        //StartCoroutine(FadeBumper(durationTime)); Fade not working at the moment
        for (int i = 0; i < activeBumpers.Length; i++)
        {
            Destroy(activeBumpers[i]);
        }

    }

    void OnCollisionExit(Collision collisionInfo)
    {
        Invoke("DestroyBumpers", 1.2f);
    }
}