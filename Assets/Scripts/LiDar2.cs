namespace VRTK.Examples
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;

//README !Important!
//
//The following scripts lidar via raycasting only works if the intended hit-gameobject has an attached collider.
//Gameobjects which shall be ignored by the raycast need to be set to Layer8.
//Fading only works if the dot materials shader is set to transparent.
//The script has to be applied on the gameobject, they ray shoots from, since this is the start point of the ray and transform.position for distance calculation of the lidar.
//Reading Textures attributes, such as Color is only possible if the textures import setting Read/Write is enabled! 

public class LiDar2 : MonoBehaviour
{
    [Header("Lidar General Settings")]
    public VRTK_InteractableObject lidarPistol;
    public GameObject dot;
    [Tooltip("Delay/Break in seconds after previous shot")]
    [Range(0.0f, 20.0f)]
    public float shotDelay = 0.0f;
    private int rows = 400;
    private int columns = 400;
    private List<GameObject> dots = new List<GameObject>();
    private GameObject gridParent;
    private bool allowShoot = true;
    private float rayLength = 50.0f;

    [Header("Lidar Color Settings (Over Distance)")]
    public bool enableDistanceColoring = false;
    public Color startColor;
    public Color endColor;
    [Range(0.1f, 3.0f)]
    public float blendFactor = 1.0f;

    [Header("Lidar Fade Settings")]
    public bool enableFading = false;
    public Material dotMaterial;
    private Color dotColor;
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

    [Header("Lidar Limitation Settings")]
    [Range(0.0f, 1f)]
    public float metallicLimit = 0.0f;
    [Range(0.0f, 1f)]
    public float glossinessLimit = 0.0f;
    [Range(0.6f, 1.5f)]
    public float noiseOffsetMin = 0.0f;
    [Range(0.6f, 1.5f)]
    public float noiseOffsetMax = 0.0f;
    [Range(0.0f, 1.0f)]
    public float transparencyLimit = 0.0f;

        //container for method ActivateLidar 
        private RaycastHit hit;
        private Material rendMat;
        private GameObject dot2;
        private Vector3 direction;
        private Vector3 hitLocation;
        private float randomOffset;
        private Texture2D tex;
        private Vector2 pixelUV;
        private Color colorOfPixel;
        private float smoothness;
        private float metallic;
        private bool dotActive = false;

        /// <summary>
        /// Create mesh of dots for the LiDar shader.
        /// </summary>
        void Start()
        {
            if (dotMaterial != null)
            {
                dotMaterial.SetColor("_TintColor", startColor);
            }

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
            dotColor = dotMaterial.GetColor("_TintColor");
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

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                dot2 = dots[i * rows + j];
                dot2.transform.SetParent(gridParent.transform);
                direction = Quaternion.AngleAxis(spacing * i - (columns * spacing / 2), Vector3.right) * Vector3.forward;
                direction = Quaternion.AngleAxis(spacing * j - (rows * spacing / 2), Vector3.up) * direction;
                    
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, rayLength, layerMask))
                {
                    randomOffset = Random.Range(noiseOffsetMin, noiseOffsetMax);
                    hitLocation = transform.TransformDirection(direction) * hit.distance;
                    rendMat = hit.transform.GetComponent<Renderer>().material;

                        if ((rendMat.HasProperty("_Color")) && (rendMat.color.a < transparencyLimit))
                        {
                            dotActive = false;     
                        }
                        else if ((rendMat.HasProperty("_Metallic") && rendMat.HasProperty("_Glossiness")) && (rendMat.GetFloat("_Metallic") > metallicLimit) && (rendMat.GetFloat("_Glossiness") > glossinessLimit))
                        {
                            dot2.transform.position = transform.position + (hitLocation * randomOffset);
                            dotActive = true;
                        }
                         else if ((rendMat.HasProperty("_MetallicGlossMap")) && (rendMat.GetTexture("_MetallicGlossMap") != null) && (rendMat.GetTexture("_MetallicGlossMap").isReadable))
                         {
                             tex = rendMat.GetTexture("_MetallicGlossMap") as Texture2D;
                             pixelUV = hit.textureCoord;
                             pixelUV.x *= tex.width;
                             pixelUV.y *= tex.height;
                             colorOfPixel = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                             smoothness = colorOfPixel.a;
                             metallic = colorOfPixel.r;
                             if ((metallic > metallicLimit) && (smoothness > glossinessLimit))
                             {
                                 dot2.transform.position = transform.position + (hitLocation * randomOffset);
                             }
                             dotActive = true;
                         }
                        else
                        {
                            dot2.transform.position = transform.position + hitLocation;
                            dotActive = true;
                        }
                        if (dotActive)
                        {
                            dot2.SetActive(true);
                        }
                    // Coloring Grid over Distance
                   if (enableDistanceColoring)
                        {
                            var lerp = Normalize(hit);
                            dotColor = Color.Lerp(dotMaterial.GetColor("_TintColor"), endColor, lerp);
                            dot2.GetComponent<Renderer>().material.SetColor("_TintColor", dotColor);
                        }
                    }
                    else
                    {
                    dot2.SetActive(false);
                    }
            }
        }
            if (enableFading)
            {
                StartCoroutine("FadeDots");
            }
        }

         IEnumerator FadeDots()
        {
            float flag = 0;
            while (flag < fadeDuration)
            {
                flag += Time.deltaTime * fadeSpeed;
                float alpha = Mathf.Lerp(alphaStart, alphaEnd, flag / fadeDuration);
                if (enableDistanceColoring)
                {
                    GameObject[] activeDots = GameObject.FindGameObjectsWithTag("GridDot");
                    foreach (GameObject g in activeDots)
                    {
                        Material gMat = g.GetComponent<Renderer>().material;
                        g.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(gMat.GetColor("_TintColor").r, gMat.GetColor("_TintColor").g, gMat.GetColor("_TintColor").b, alpha));
                    }
                }
                else
                {
                    dotMaterial.SetColor("_TintColor", new Color(dotMaterial.GetColor("_TintColor").r, dotMaterial.GetColor("_TintColor").g, dotMaterial.GetColor("_TintColor").b, alpha));
                }
                //m.color = Color.Lerp(new Color(m.color.r, m.color.g, m.color.g, 1.0f), new Color(m.color.r, m.color.g, m.color.g, 0.0f), flag / fadeDuration); Used for standardshader, which accesses color variable.
                yield return null;
            }
            DeactivateCurrentLidar();
            Invoke("EnableShooting", shotDelay);
        }

        void EnableShooting()
        {
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
            var lerp = 1f - ((1f / (1f + hit.distance)) * blendFactor); 
            return lerp;
        }
  }
}