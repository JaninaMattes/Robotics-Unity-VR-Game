using UnityEngine;

public class BumperSpawner : MonoBehaviour
{
    //Cube prefab
    public GameObject bumper;
    private GameObject bumperClone;
    private GameObject[] controllerobjects;
    private Color alphaColor;
    private float timeToFade = 5f;
    private static float life = 100f; 

    /// <summary>
    /// Load on start
    /// </summary>
    public void Start()
    {
        if (controllerobjects.Length == 0)
        {
            controllerobjects = GameObject.FindGameObjectsWithTag("Controller");
            Debug.Log("Retrieved controller objects = " + controllerobjects.Length);
        }

        Debug.Log("Controller Objects old = " + controllerobjects.Length);

        alphaColor = bumper.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
        bumper.SetActive(false);
        // Debugging purpose
        Debug.Log("Set bumper active ");
    }

    /// <summary>
    /// On Collision Enter a new bumper object will be 
    /// instantiated and attached to the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
            if (col.gameObject == controllerobjects[0])
            {
                bumper.SetActive(true);
                Vector3 contact = col.contacts[0].point;
                contact.z = contact.z + 1; // TODO adjust  
                bumperClone = Instantiate(bumper, contact, Quaternion.identity);

                // Debugging purpose
                Debug.Log("Collision - Object in contact on x = "
                    + contact.x + "y = " + contact.y + " z = " + contact.z);
            }                      
    }

    /// <summary>
    /// OnCollisionExit is called if Collision with object
    /// has ended and the bumper instance gets destroied. 
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionExit(Collision collisionInfo)
    {
        bumper.GetComponent<MeshRenderer>().material.color = 
            Color.Lerp(bumper.GetComponent<MeshRenderer>().
            material.color, alphaColor, timeToFade * Time.deltaTime);
        bumper.SetActive(false);
        controllerobjects = null;

        if (bumperClone != null)
        {
            GameObject.Destroy(bumperClone);
            Debug.Log("Gameobject is destroied");
        }
        // Debugging purpose
        Debug.Log("No longer in contact with " + collisionInfo.transform.name);
    }
}
