using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicerRocket : MonoBehaviour
{

    Rigidbody rb;

    public ParticleSystem funkenParticleSystem;
    public GameObject explosionPrefab;
    ParticleSystem sparkParticles;
    ParticleSystem smokeParticles;


    public float force = 10f;
    //public float noiseStrength = 1;
    public float torqueStrength = 1;

    public float startTimer = 3;
    public float duration = 4;
    public float timeUntilExplosion = 1f;

    bool engaged;

   

    // Start is called before the first frame update
    void Start()
    {
        smokeParticles = funkenParticleSystem.transform.GetChild(0).GetComponent<ParticleSystem>();
        sparkParticles = funkenParticleSystem.transform.GetChild(1).GetComponent<ParticleSystem>();

        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.centerOfMass.Set(0, 0.23f, 0);

        funkenParticleSystem.gameObject.SetActive(false);




    }

    public void Ignite()
    {
        if (!engaged)
        {
            funkenParticleSystem.gameObject.SetActive(true);
            Invoke("EngageRocket", startTimer);
            Invoke("DisengageRocket", startTimer + duration);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (!engaged)
    //        {
    //            funkenParticleSystem.gameObject.SetActive(true);
    //            Invoke("EngageRocket", startTimer);
    //            Invoke("DisengageRocket", startTimer + duration);
    //        }
    //    }
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (engaged)
        {
            rb.AddRelativeForce(new Vector3(Random.Range(-0.1f, 0.1f), 1,Random.Range(-0.1f,0.1f)) * force);
            rb.AddRelativeTorque(Vector3.up * torqueStrength);

            //if (rb.velocity.magnitude > maxVelocity)
            //{
            //    Vector3 newVelocity = rb.velocity.normalized;
            //    newVelocity *= maxVelocity;
            //    rb.velocity = newVelocity;
            //}
        }
    }

    void EngageRocket()
    {
        var sparkMain = sparkParticles.main;
        var sparkEmission = sparkParticles.emission;

        sparkMain.startLifetimeMultiplier = 0.8f;
        sparkEmission.rateOverTimeMultiplier = 600f;
        sparkMain.startSizeMultiplier = 0.03f;

        var smokeMain = smokeParticles.main;

        smokeMain.startColor = new Color(0.75f, 0.75f, 0.75f);

        engaged = true;
        rb.useGravity = false;
    }

    void DisengageRocket()
    {
        engaged = false;
        sparkParticles.gameObject.SetActive(false);

        var smokeMain = smokeParticles.main;
        smokeMain.startColor = new Color(0.35f, 0.35f, 0.35f);

        rb.useGravity = true;
        Invoke("Explode", timeUntilExplosion);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0,0,0));
        Destroy(gameObject);
    }
}
