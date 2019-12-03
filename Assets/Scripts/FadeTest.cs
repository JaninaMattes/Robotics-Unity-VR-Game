using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    public float fadeDuration; 
    public float fadeStartValue; // Has to be minimum 0.5f and maximum 8.0f, since this is defined as a range in the Hologramshader itself. The rimpower/color intensity is brightest at 0.5f
    public float fadeEndValue; // Has to be minimum 0.5f and maximum 8.0f, since this is defined as a range in the Hologramshader itself.
    public float fadeSpeed;

   void Awake()
    {
        StartCoroutine(FadeBumper(fadeDuration)); // Can be called by StartCoroutine from everywhere to start the IENumerator 
    }

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
    }
}
