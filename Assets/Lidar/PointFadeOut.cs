using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFadeOut : MonoBehaviour
{

    public int speed = 3;
    Color initalCol;


    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initalCol = meshRenderer.material.GetColor("_UnlitColor");
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", speed, "easetype", "easeInCubic", "onUpdate", "UpdateColor", "oncomplete", "DestroySelf", "oncompletetarget", gameObject));
    }

    void UpdateColor(float newAlpha)
    {
        meshRenderer.material.SetColor("_UnlitColor", new Color(initalCol.r, initalCol.g, initalCol.b, newAlpha));
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
