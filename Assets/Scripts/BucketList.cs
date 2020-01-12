using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[ExecuteInEditMode]
public class BucketList : MonoBehaviour
{
    [Header("List of Interactable Objects")]
    public VRTK_InteractableObject bucketlist;
    [Tooltip("Bucket List Objects")]
    public VRTK_InteractableObject bucket;
    [Tooltip("Bucket List Objects")]
    public List<string> bucketList_Tags = new List<string>(); // Tags about which object is allowed

    protected struct GameObjectData
    {
        GameObject collectedObject;
        string objectTag;
        int objectCollected; // How often object got collected + classified

        public void SetGameObject(GameObject gameObject)
        {
            this.collectedObject = gameObject;
            this.objectTag = collectedObject.tag;
        }

        public void AddCollectionPoint()
        {
            ++ objectCollected;
        }
    }

    // Dictionary to store all Gameobjects that are in the bucket with their tags
    protected Dictionary<string, GameObjectData> bucket_collection = new Dictionary<string, GameObjectData>();
    protected Game_Manager controller = Game_Manager.Instance;

    public void OnEnable()
    {
        if (bucket != null)
        {
            bucket.InteractableObjectUsed += InteractableObjectUsed;
            bucket.InteractableObjectUnused += InteractableObjectUnused;
        }

    }

    public void OnDisable()
    {
        if (bucket != null)
        {
            bucket.InteractableObjectUsed -= InteractableObjectUsed;
            bucket.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        //bucket.GetTouchingObjects();
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
    }

    /// <summary>
    /// When Object is dropped into Bucket
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            //audioSource.Play();
        }

        // Call all objects in Bucket
        var allObjects = bucket.GetTouchingObjects();
        foreach (GameObject obj in allObjects)
        {
            var distance = Vector2.Distance(obj.transform.localPosition, collision.transform.position);
            if (distance < 0.01)
            {
                CheckObjects(obj);
            }
        }       
    }

    private void CheckObjects(GameObject gameObject)
    {
        GameObjectData gameObj = new GameObjectData();
        if (bucketList_Tags.Contains(gameObject.tag))
        {
            gameObj.SetGameObject(gameObject);
            bucket_collection.Add(gameObject.tag, gameObj);
            //bucketlist set Checklist element of object with certain tag to true
            //base.ResetMaterial(gameObject); // TODO: Architektur - Design Pattern !!! 
            //controller.learnedObj.Add();
            ++ controller.playerScore;
        } 
    }
}
