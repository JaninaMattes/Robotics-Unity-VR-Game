using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Material hit;
    public Material hitandshot;
    public Material notHit;

    private bool _isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_isHit)
        {
            GetComponent<Renderer>().material = hit;
        }
        else
        {
            GetComponent<Renderer>().material = notHit;
        }

        if(OVRInput.Get(OVRInput.Button.One))
        {
            GetComponent<Renderer>().material = hitandshot;
        }
        else
        {
            GetComponent<Renderer>().material = notHit;
        }

        _isHit = false;

    }


    public void BeenHit()
    {
        _isHit = true;
    }
}
