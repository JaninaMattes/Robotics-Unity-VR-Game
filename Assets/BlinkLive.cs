using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkLive : MonoBehaviour
{
    public RawImage image;

    void OnEnable()
    {
        StartBlinking();
    }


    IEnumerator Blink()
    {
        while (true)
        {
            switch (image.color.a.ToString())
            {
                case "0":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    yield return new WaitForSeconds(0.9f);
                    break;
                case "1":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    yield return new WaitForSeconds(0.6f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        //StopAllCoroutines();
        StartCoroutine("Blink");
    }

    void StopBlinking()
    {
        StopAllCoroutines();
    }

}
 


