using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class BumperSpawner : MonoBehaviour
{
    // All gameobjects that can be used
    public GameObject circularBumper;
    public GameObject groundPrefab;

    // All settings for Bumper
    public float distance;
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
        // playerLocation = OVRManager.tracker.GetPose().position;
        cameraRigTransform = GameObject.FindGameObjectWithTag("MainCamera");
        playerLocation = cameraRigTransform.transform.position; //returns found VRSimulatorCameraRig if it is found

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
                activeBumper = Instantiate(circularBumper, new Vector3(contact.x, Camera.main.transform.position.y - 0.7f, contact.z) + 
                               Camera.main.transform.forward * distance, Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// OnCollisionExit is calledf when collision is ended
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionExit(Collision collisionInfo)
    {
        if (activeBumper != null)
        {
            Destroy(activeBumper);
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
        }
        else
        {
           // groundOrientation.transform.position.x = playerLocation.transform.position.x;
           // groundOrientation.transform.position.z = playerLocation.transform.position.x;
           // groundOrientation.transform.position.y = 0.1f;
        }
    }
    
    /// <summary>
    /// Delete instance of ground orientation prefab. 
    /// </summary>
    void DestroyGround()
    {
        Destroy(groundOrientation);
    }
}