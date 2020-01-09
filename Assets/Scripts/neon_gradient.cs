using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neon_gradient : MonoBehaviour
{

    public float phaseOffset = 0.1f;
    public float phaseLength = 1f;
    public Material[] materials;

    [ColorUsage(false,true)]
    public Color stdCol;


    // Start is called before the first frame update
    void Start()
    {
        //stdCol = materials[0].GetColor("_EmissionColor");
        //Debug.Log(stdCol);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            float sinVal = (Mathf.Sin((Time.time*phaseLength)+phaseOffset*i)+1)/2;
            if (sinVal>0.5)
            {
                sinVal = 1;
            }
            else
            {
                sinVal = 0;
            }
            Color newCol = stdCol * sinVal;
            materials[i].SetColor("_EmissionColor", newCol);
        }
       // Debug.Log((Mathf.Sin((Time.time * phaseLength) + phaseOffset)+1)/2);
    }
}
