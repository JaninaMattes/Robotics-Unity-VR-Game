using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarShader_Object : MonoBehaviour
{

    /// <summary> 
    /// Class <c> SonarShaderObject </c>
    /// Renderer takes all the sonar data sent to the shader
    /// QueueSize contains the number of rings that can be
    /// rendered at once and needs to be the same value as
    /// the array size in the shader file itself. 
    /// The Queue of start positions of sonar rings is 
    /// described within the positionQueue. 
    /// The xyz values hold the xyz position
    /// The w value holds the time that position was started at
    /// </summary>
    
    // all global properties
    private Renderer[] ObjectRenderer;
    private static readonly Vector4 GarbagePosition = new Vector4(-5000, -5000, -5000, -5000);
    private static int QueueSize = 20;
    private static Queue<Vector4> positionQueue = new Queue<Vector4>(QueueSize);
    private static Queue<float> intensityQueue = new Queue<float>(QueueSize);
    private static bool NeedToIntQueues = true;

    // calls the SendSonarData for each object
    private delegate void Delegate();
    private static Delegate RingDelegate;

    ///<summary> 
    ///Start is called first </summary>
    void Start()
    {
        // get all renderer objects that will have the effect applied
        ObjectRenderer = GetComponentsInChildren<Renderer>();

        if (NeedToIntQueues)
        {
            NeedToIntQueues = false;
            for (int i = 0; i < QueueSize; i++)
            {
                positionQueue.Enqueue(GarbagePosition);
                intensityQueue.Enqueue(-5000.0f);
            }
        }

        // add object functions to static delegate
        RingDelegate += SendSonarData;
    }

    ///
    /// <summary> The function sendSonarData() sends sonar data to the shader
    /// </summary>
    private void SendSonarData()
    {
        foreach (Renderer render in ObjectRenderer)
        {
            render.material.SetVectorArray("_hitPts", positionQueue.ToArray());
            render.material.SetFloatArray("_Intensity", intensityQueue.ToArray());
        }
    }

    void OnCollisionEnter(Collision col)
    {
        StartSonarRing(col.contacts[0].point, col.impulse.magnitude / 10.0f);
    }
}
