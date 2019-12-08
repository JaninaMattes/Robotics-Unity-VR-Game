using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BumperSpawner : MonoBehaviour
{
    public GameObject bumperClone;
    public float distance;
    public float fadeDuration;
    public float fadeStartValue; // Has to be minimum 0.5f and maximum 8.0f, since this is defined as a range in the Hologramshader itself. The rimpower/color intensity is brightest at 0.5f
    public float fadeEndValue; // Has to be minimum 0.5f and maximum 8.0f, since this is defined as a range in the Hologramshader itself.
    public float fadeSpeed;
    private GameObject[] activeBumpers;
  
    void Update()
    {
        activeBumpers = GameObject.FindGameObjectsWithTag("Bumper");
    

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
            if (activeBumpers.Length == 0)
            {
                Instantiate(bumperClone, new Vector3(contact.x, Camera.main.transform.position.y - 0.7f, contact.z) + 
                    Camera.main.transform.forward * distance, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// Fade object out over certain time.
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>

    IEnumerator FadeBumper(float duration)
    {
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime * fadeSpeed;
            float rimPowerShader = Mathf.Lerp(fadeStartValue, fadeEndValue, counter / duration); //Lerping the value of the rimpower between a given start- and endvalue
            gameObject.GetComponent<Renderer>().material.SetFloat("_RimPower", rimPowerShader); // Set the RimPower of the Bumpers attached shader
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

    /// <summary>
    /// Delete Bumper instance. 
    /// </summary>
    void DestroyBumpers()
    {
        StartCoroutine(FadeBumper(fadeDuration)); // Can be called by StartCoroutine from everywhere to start the IENumerator 
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        Invoke("DestroyBumpers", 1.2f);
    }    
}