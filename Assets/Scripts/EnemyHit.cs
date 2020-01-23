using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Material hit;
    public Material hitandshot;
    public Material notHit;

    private bool _isHit = false;
    private bool _isShot = false;

    public GameObject replacement;
    public GameObject PointCounter;

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

        if (_isShot)
        {
            GetComponent<Renderer>().material = hitandshot;
            //Debug.Log("Erschossen");

            GameObject.Instantiate(replacement, transform.position, transform.rotation);
            Destroy(gameObject);
            GameObject.Instantiate(PointCounter, transform.position, transform.rotation);


        }

        else
        {
            GetComponent<Renderer>().material = notHit;
        }

        _isHit = false;
        _isShot = false;

    }


    public void BeenHit()
    {
        _isHit = true;
    }

    public void BeenShot()
    {
        _isShot = true;
    }
}
