using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeTillDestroy = 2f;
    // Start is called before the first frame update

    void OnEnable()
    {
        Destroy(gameObject, timeTillDestroy);
    }

}
