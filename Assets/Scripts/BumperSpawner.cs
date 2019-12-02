using UnityEngine;

public class BumperSpawner : MonoBehaviour
{
    //Cube prefab
    public GameObject bumper;
    public GameObject bumperClone;
    public GameObject[] controllerobjects;
    private Color alphaColor;
    private float timeToFade = 1f;
    private static float life = 100f; 

    /// <summary>
    /// Load on start
    /// </summary>
    public void Start()
    {
        if (controllerobjects == null)
        {
            controllerobjects = GameObject.FindGameObjectsWithTag("Controller");
            Debug.Log("Retrieved controller objects = " + controllerobjects.Length);
        }

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
        if(col.gameObject == controllerobjects[0])
        {
            bumper.SetActive(true);
            Vector3 contact = col.contacts[0].point;
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

        if (bumperClone != null)
        {
            GameObject.Destroy(bumperClone);
            Debug.Log("Gameobject is destroied");
        }
        // Debugging purpose
        Debug.Log("No longer in contact with " + collisionInfo.transform.name);
    }
}
