namespace VRTK.Examples
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;
    using System;

    public class LiDar : MonoBehaviour
{
    [Header("Lidar General Settings")]
    public VRTK_InteractableObject lidarPistol;
    public GameObject dot;
    //public Color colorStart;
    //public Color colorEnd;
    private int rows = 400;
    private int columns = 400;
    private List<GameObject> dots = new List<GameObject>();
    private GameObject gridParent;
    private bool allowShoot = true;

    [Header("Lidar Fade Settings")]
    public Material dotMaterial;
    [Range(0.1f, 10.0f)]
    public float fadeDuration;
    [Range(0.1f, 10.0f)]
    public float fadeSpeed;
    private const float alphaStart = 1.0f;
    private const float alphaEnd = 0.0f;

    [Header("Lidar FoV Settings")]
    [Range(1.0f, 360f)]
    public float angle;
    [Tooltip("Angle is only correct if Spacing set to 1")]
    [Range(0.1f, 5f)]
    public float spacing = 1.0f; //angle only correct if spacing set to 1.
    private float angleFactor;
    private const int maxRows = 400;

    [Header("Color Setting")]
    public Color errorColor = new Color(1, 0, 0); // default red
    public Color startColor = new Color(0, 0, 0);
    public Color endColor = new Color(0, 0, 0);

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
            if (allowShoot)
            {
                ActivateLidar();
                allowShoot = false;
            }
        }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {  
        }

    void DeactivateCurrentLidar()
        {
            GameObject[] activeDots = GameObject.FindGameObjectsWithTag("GridDot");
            foreach (GameObject g in activeDots)
            {
                g.SetActive(false);
            }
        }

        void ActivateLidar()
        {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        Renderer renderer;
        Material material;
        MeshRenderer mesh;
        Color setColor;
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
                    dot.transform.position = transform.position + hitLocation;
                    // Calls the Renderer of the Collider that was hit
                    renderer = hit.collider.GetComponent<Renderer>();
                    // Calls the Material of the hit Renderer
                    material = renderer.material;
                    mesh = dot.GetComponent<MeshRenderer>();

                        if (material.GetFloat("_Shininess") > 0)
                        {
                            setColor = errorColor;
                        }
                        else
                        {
                            setColor = startColor;
                        }
                        mesh.material.SetColor("_TintColor", setColor);

                        //MeshRenderer mesh = dot.GetComponent<MeshRenderer>();
                        //var lerp = Normalize(hit);
                        //mesh.material.color = Color.Lerp(colorStart, colorEnd, lerp);
                        dot.SetActive(true);    
                }
                    else
                    {
                    dot.SetActive(false);
                    }
            }
        }
            StartCoroutine("FadeDots");
        }

         IEnumerator FadeDots()
        {
            float flag = 0;
            while (flag < fadeDuration)
            {
                flag += Time.deltaTime * fadeSpeed;
                float alpha = Mathf.Lerp(alphaStart, alphaEnd, flag / fadeDuration);
                dotMaterial.SetColor("_TintColor", new Color(dotMaterial.GetColor("_TintColor").r, dotMaterial.GetColor("_TintColor").g, dotMaterial.GetColor("_TintColor").b, alpha));
                //m.color = Color.Lerp(new Color(m.color.r, m.color.g, m.color.g, 1.0f), new Color(m.color.r, m.color.g, m.color.g, 0.0f), flag / fadeDuration); Used for standardshader, which accesses color variable.
                yield return null;
            }
            DeactivateCurrentLidar();
            allowShoot = true;
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