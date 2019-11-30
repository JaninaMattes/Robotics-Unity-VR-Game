using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarState : MonoBehaviour
{
    // Properties
    public float speed;
    public Material material;
    public Vector4 sonarOrigin = Vector4.one;
<<<<<<< Updated upstream:Assets/Scripts/SonarState.cs
=======
    public Vector4[] testa;
    public Vector4[] testD = new Vector4 [20];
    public Vector4 testb;
    public Vector4[] testF;
    public float testc;


>>>>>>> Stashed changes:Assets/Scripts/SonarState2.cs

    // Setter and Getter
    public Vector3 SonarOrigin { set { sonarOrigin = new Vector4(value.x, value.y, value.z, 0); } }

    public void LiToAr()
    {
        testD = gameObject.GetComponent<SonarCreator2>().contacts.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream:Assets/Scripts/SonarState.cs
        if (gameObject.GetComponent<SonarCreator>().switchSonar)
=======
        if(gameObject.GetComponent<SonarCreator2>().contacts.ToArray().Length > 0)
        {
            LiToAr();
        }
        //testF = gameObject.GetComponent<SonarCreator2>().contacts.ToArray();
        //for (int i = 0; i < gameObject.GetComponent<SonarCreator2>().contacts.Count; ++i)
        //material.SetVector("_myVectorArray" + i, gameObject.GetComponent<SonarCreator2>().contacts[0]);
        
        testa = material.GetVectorArray("_myVectorArray");
       // testb = testa[0];
       // testc = testa.Length;
        if (gameObject.GetComponent<SonarCreator2>().switchSonar)
>>>>>>> Stashed changes:Assets/Scripts/SonarState2.cs
        {
            sonarOrigin = gameObject.GetComponent<SonarCreator>().firstCollision;
        }
        // If sonar ring exceedes one it gets reduced 
        // so that colors are not inverted
        for (int i = 0; i < testD.Length; i++)
        {
            testD[i].w = Mathf.Min(testD[i].w + (Time.deltaTime * speed), 1);
       }
        //testD[0].w = Mathf.Min(testD[0].w + (Time.deltaTime * speed), 1);
        //sonarOrigin.w = Mathf.Min(sonarOrigin.w + (Time.deltaTime * speed), 1);
        
            material.SetVectorArray("_myVectorArray", testD);
        
        //material.SetVector("_SonarOrigin", sonarOrigin);
    }
}
