using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarLaser : MonoBehaviour
{
    private LineRenderer lr;
    public Material material;
    public Vector4 sonarOrigin = Vector4.one;
    public float speed;
    bool test = false;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }
    
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            lr.enabled = true;
            StartCoroutine(WaitSonarShot());
            lr.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //if (hit.collider) Überprüfung ob getroffenes GameObject einen Collider hat / Auf Kollision überprüfen
                //{
                    lr.SetPosition(1, hit.point);
                sonarOrigin = hit.point;
                //}
            }
            else lr.SetPosition(1, transform.forward * 5000);
        }

        sonarOrigin.w = Mathf.Min(sonarOrigin.w + (Time.deltaTime * speed), 1);
        material.SetVector("_SonarOrigin", sonarOrigin);
    }

    IEnumerator WaitSonarShot()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        lr.enabled = false;
    }
}
