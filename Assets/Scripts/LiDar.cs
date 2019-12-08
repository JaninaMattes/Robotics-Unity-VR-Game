namespace VRTK.Examples
{
    using System.Collections.Generic;
using UnityEngine;

public class LiDar : MonoBehaviour
{
    public GameObject dot;
    public Color colorStart;
    public Color colorEnd;
    private bool dotsActive = false;
    public int rows = 100;
    public int columns = 100;
    public float spacing = 20.0f;
    private List<GameObject> dots = new List<GameObject>();
    public VRTK_InteractableObject lidarPistol;

        /// <summary>
        /// Create mesh of dots for the LiDar shader.
        /// </summary>
        void Start()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject temp = Instantiate(dot, transform.position, Quaternion.identity);
                temp.name = "i : " + i + " , j : " + j;
                dots.Add(temp);
            }
        }
    }

    protected virtual void OnEnable()
    {
            lidarPistol = (lidarPistol == null ? GetComponent<VRTK_InteractableObject>() : lidarPistol);

        if (lidarPistol != null)
        {
                lidarPistol.InteractableObjectUsed += InteractableObjectUsed;
                lidarPistol.InteractableObjectUnused += InteractableObjectUnused;
        }

    }

    protected virtual void OnDisable()
    {
        if (lidarPistol != null)
        {
                lidarPistol.InteractableObjectUsed -= InteractableObjectUsed;
                lidarPistol.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }


    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
            dotsActive = true;
        }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
            dotsActive = false;
        }

    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject dot = dots[i * rows + j];
                Vector3 direction = Quaternion.AngleAxis(spacing * i - (columns * spacing / 2), Vector3.right) * Vector3.forward;
                direction = Quaternion.AngleAxis(spacing * j - (rows * spacing / 2), Vector3.up) * direction;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity, layerMask))
                {
                    Vector3 hitLocation = transform.TransformDirection(direction) * hit.distance;
                    // For debugging purpose to show rays Debug.DrawRay
                    // Debug.DrawRay(transform.position, hitLocation, Color.yellow);
                    dot.transform.position = transform.position + hitLocation;
                        if (dotsActive)
                        {
                            MeshRenderer mesh = dot.GetComponent<MeshRenderer>();
                            mesh.material.color = Color.Lerp(colorStart, colorEnd, hit.distance);
                            Debug.Log("Color "+ mesh.material.color);
                            dot.SetActive(true);
                        }
                        else {
                            dot.SetActive(false);
                        }
                }
                else
                {
                        // For debugging purpose to show rays Debug.DrawRay
                        // Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1000, Color.white);
                        dot.SetActive(false);
                }
            }
        }
    }
}
}