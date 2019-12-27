using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class BumperSpawner : MonoBehaviour
{
    // All gameobjects that can be used
    public GameObject bumper;
    public GameObject groundPrefab;
    // All settings for Bumper
    public float distance;
    public float fadeDuration;
    public float fadeStartValue; // Has to be minimum 0.5f and maximum 8.0f, since this is defined as a range in the Hologramshader itself. The rimpower/color intensity is brightest at 0.5f
    public float fadeEndValue; // Has to be minimum 0.5f and maximum 8.0f, since this is defined as a range in the Hologramshader itself.
    public float fadeSpeed;
    public int sceneIndex;
    
    // store new GameObject instance
    private GameObject activeBumper;
    private Scene activeScene;
    private GameObject groundOrientation;
    private Vector3 playerLocation;
    private GameObject cameraRigTransform;

    // Called once per frame
    void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        sceneIndex = activeScene.buildIndex;
        Debug.Log($"Active Scene Index {activeScene.buildIndex}");
        // Retrieve Player Headset position
        //playerLocation = OVRManager.tracker.GetPose().position;
        cameraRigTransform = GameObject.FindGameObjectWithTag("MainCamera");
        playerLocation = cameraRigTransform.transform.position; //returns found VRSimulatorCameraRig if it is found
        Debug.Log($"Player position {playerLocation}");

        // Check Scene index returns an integer value
        if (activeScene.buildIndex != 0)
        {
            // Activate Controller in Minigames
            ActivateCollider(true);
            SetGroundOrientation();
        }
        else
        {
            // Deactivate Controller in Workshop
            ActivateCollider(false);
            DestroyGround();
        }
    }
  
    /// <summary>
    /// On Collision Enter a new bumper object will be 
    /// instantiated and attached to the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collission detected");
        Vector3 contact = col.contacts[0].point;
        // Check the scene index
        if (activeScene.buildIndex != 0)
        {
            if (activeBumper == null)
            {
                activeBumper = Instantiate(bumper, new Vector3(contact.x, Camera.main.transform.position.y - 0.7f, contact.z) + 
                               Camera.main.transform.forward * distance, Quaternion.identity);
                Debug.Log("New bumper instance " + activeBumper);
            }
        }
    }

    /// <summary>
    /// OnCollisionExit is calledf when collision is ended
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionExit(Collision collisionInfo)
    {
        // Use reflection via Invoke 
        Invoke("DestroyBumpers", 1.2f);
    }

    /// <summary>
    /// Coroutines allow to use procedural animations or sequences of events over time
    /// Otherwise functions need happen only within a single frame.
    /// The function FadeBumper allows to fade the bumper object out over certain time.
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>

    IEnumerator FadeBumper()
    {
        // Check if bumper instance is empty
        if(activeBumper != null)
        {
            float flag = 0; 
            while(flag < fadeDuration)
            {
                Debug.Log("Coroutine called ");
                flag = Time.deltaTime * fadeSpeed;
                float rimPowerShader = Mathf.Lerp(fadeStartValue, fadeEndValue, flag / fadeDuration); //Lerping the value of the rimpower between a given start- and endvalue
                activeBumper.GetComponent<Renderer>().material.SetFloat("_RimPower", rimPowerShader); // Set the RimPower of the Bumpers attached shader
                yield return null; // needs to be placed where execution will be paused and resumed on the following frame
            }
            if(flag == fadeDuration)
            {
                // After while loop has faded bumper out call Destroy(Bumper)
                Destroy(activeBumper);
                Debug.Log("Bumper is destroied");
            }            
        }
        else
        {
            Debug.Log("No bumper instance");
        }        
    }
    
    /// <summary>
    /// Activate the Collider of the associated Controller.
    /// </summary>
    /// <param name="enable"></param>
    void ActivateCollider(bool enable)
    {
        // Set all Colliders inactive on the gameobjects
        GetComponent<BoxCollider>().enabled = enable;
        Debug.Log($"Set Collider {enable}");        
    }

    /// <summary>
    /// Instanciate the ground orientation prefab and attach it to user
    /// </summary>
    /// <param name="enable"></param>
    void SetGroundOrientation()
    {
        if (groundOrientation == null)
        {
            groundOrientation = Instantiate(groundPrefab, playerLocation, Quaternion.identity);
            Debug.Log("Ground Orientation instance created.");
        }
        else
        {
           // groundOrientation.transform.position.x = playerLocation.transform.position.x;
           // groundOrientation.transform.position.z = playerLocation.transform.position.x;
           // groundOrientation.transform.position.y = 0.1f;
        }
    }

    /// <summary>
    /// Fade Bumper out by calling Coroutine 
    /// and destroying the instance.
    /// </summary>
    void DestroyBumpers()
    {
        StartCoroutine("FadeBumper"); // Can be called by StartCoroutine from everywhere to start the IENumerator 
    }

    /// <summary>
    /// Delete instance of ground orientation prefab. 
    /// </summary>
    void DestroyGround()
    {
        Destroy(groundOrientation);
        Debug.Log("Ground orientation prefab destroied");
    }
}