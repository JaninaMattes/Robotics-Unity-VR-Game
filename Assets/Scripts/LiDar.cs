namespace VRTK.Examples
{
    using System.Collections.Generic;
using UnityEngine;

public class LiDar : MonoBehaviour
{
    public GameObject dot;
    //public Color colorStart;
    //public Color colorEnd;
    private bool dotsActive = false;
    private int rows = 400;
    private int columns = 400;
    private List<GameObject> dots = new List<GameObject>();
    public VRTK_InteractableObject lidarPistol;
    private GameObject gridParent;

    [Header("Lidar FoV")]
    [Range(1.0f, 360f)]
    public float angle;
    [Tooltip("Angle is only correct if Spacing set to 1")]
    [Range(0.1f, 5f)]
    public float spacing = 1.0f; //angle only correct if spacing set to 1.
    private float angleFactor;
    private const int maxRows = 400;

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
            gridParent = GameObject.FindGameObjectWithTag("Grid");
     }

        void SetGrid()
        {
            if ((maxRows > 0) && (angle > 0))
            {
                angleFactor = (360.0f / (float)maxRows);
                rows = Mathf.RoundToInt(angle / angleFactor);
                columns = rows;
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
            SetGrid();
            dotsActive = true;
            ActivateLidar();
        }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
            dotsActive = false;
        }

    void ActivateLidar()
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
                dot.transform.SetParent(gridParent.transform);
                Vector3 direction = Quaternion.AngleAxis(spacing * i - (columns * spacing / 2), Vector3.right) * Vector3.forward;
                direction = Quaternion.AngleAxis(spacing * j - (rows * spacing / 2), Vector3.up) * direction;
                // Does the ray intersect any objects excluding the player layer
               
                if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity, layerMask))
                {

                    Vector3 hitLocation = transform.TransformDirection(direction) * hit.distance;

                        Debug.DrawRay(transform.position, hitLocation, Color.red);
                        // For debugging purpose to show rays Debug.DrawRay
                        // Debug.DrawRay(transform.position, hitLocation, Color.yellow);
                        dot.transform.position = transform.position + hitLocation;
                        if (dotsActive)
                        {
                            //MeshRenderer mesh = dot.GetComponent<MeshRenderer>();
                            //var lerp = Normalize(hit);
                           // mesh.material.color = Color.Lerp(colorStart, colorEnd, lerp);
                           // Debug.Log("Color "+ mesh.material.color + "Lerp Math " + lerp);
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

        /// <summary>
        /// Bind an arbitrary number to values between 0 and 1
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        public float Normalize(RaycastHit hit)
        {
            //var lerp = Mathf.PingPong(hit.distance, 1);
            var lerp = 1f - (1f / (1f + hit.distance)); 
            return lerp;
        }
  }
}