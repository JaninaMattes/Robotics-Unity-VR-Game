using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour
{

    float TimeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;

        if (TimeCounter >= 1.5)
        {
            Destroy(gameObject);
        }
    }
}
