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

    [Header("Lidar Color Settings (Over Distance)")]
    public bool enableDistanceColoring = false;
    public Color startColor;
    public Color endColor;
    [Range(0.1f, 3.0f)]
    public float blendFactor = 1.0f;

    [Header("Lidar Fade Settings")]
    public bool enableFading = false;
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

    [Header("Lidar Limitation Settings")]
    [Range(0.0f, 1f)]
    public float metallicLimit = 0.0f;
    [Range(0.0f, 1f)]
    public float glossinessLimit = 0.0f;
    [Range(0.6f, 1f)]
    public float noiseOffsetMin = 0.0f;
    [Range(0.6f, 1f)]
    public float noiseOffsetMax = 0.0f;
    [Range(0.0f, 1.0f)]
    public float transparencyLimit = 0.0f;
    private bool belowTransparencyLimit = false;


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
        Renderer rend;
        Color dotColor;

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
                    rend = hit.transform.GetComponent<Renderer>();
                    dotColor = dotMaterial.GetColor("_TintColor");

                   //Lidar Limitation checks (if enabled in inspector)
                           CheckTransparencyValue(dot, hit, rend);
                           if (belowTransparencyLimit == false)
                           {
                             CheckMetallicValue(dot, hit, rend, direction);
                           }
                        
                    // Coloring Grid over Distance
                   if (enableDistanceColoring)
                        {
                            var lerp = Normalize(hit);
                            dotColor = Color.Lerp(dotMaterial.GetColor("_TintColor"), endColor, lerp);
                            dot.GetComponent<Renderer>().material.SetColor("_TintColor", dotColor);
                        }
                    }
                    else
                    {
                    dot.SetActive(false);
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

        //Check if hit object by raycast has a material, a collider and renderer. If true, Metallic Value and MetallicGlossMap value at pixel get checked if exceeding the set metalliclimit.
        //lidarParticle/dot at this ray hit get set active afterwards.
        void CheckMetallicValue(GameObject lidarParticle, RaycastHit hitInfo, Renderer hitRenderer, Vector3 direction)
        {
            Vector3 hitLocation = transform.TransformDirection(direction) * hitInfo.distance;
            float randomOffset = Random.Range(noiseOffsetMin, noiseOffsetMax);
            //if (hitRenderer == null || hitRenderer.sharedMaterial == null || hitRenderer.GetComponent<Collider>() == null)
            //return;

            if ((hitRenderer.material.GetFloat("_Metallic") > metallicLimit) && (hitRenderer.material.GetFloat("_Glossiness") > glossinessLimit))
            {
                lidarParticle.transform.position = transform.position + (hitLocation * randomOffset);
            }
             else if ((hitRenderer.material.GetTexture("_MetallicGlossMap") != null) && (hitRenderer.material.GetTexture("_MetallicGlossMap").isReadable))
             {
                 Texture2D tex = hitRenderer.material.GetTexture("_MetallicGlossMap") as Texture2D;
                 Vector2 pixelUV = hitInfo.textureCoord;
                 pixelUV.x *= tex.width;
                 pixelUV.y *= tex.height;
                 Color colorOfPixel = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                 //float smoothness = colorOfPixel.a;
                 float metallic = colorOfPixel.r;
                if((metallic > metallicLimit) && (hitRenderer.material.GetFloat("_Glossiness") > glossinessLimit))
                {
                    lidarParticle.transform.position = transform.position + (hitLocation * randomOffset);
                }
             }
            else
            {
                lidarParticle.transform.position = transform.position + hitLocation;
            }
                 lidarParticle.SetActive(true);
        }

        //Check if hit object by raycast has a material, a collider and renderer. If true, color transparency value and AlbedoMap(mainTexture) transparency value at pixel get checked if set under the set transparencyLimit.
        //If so, the lidarParticle/dot at this ray hit get set active = false. Else it get set active = true (visible)
        void CheckTransparencyValue(GameObject lidarParticle, RaycastHit hitInfo, Renderer hitRenderer)
        {
            //if (hitRenderer == null || hitRenderer.sharedMaterial == null || hitRenderer.GetComponent<Collider>() == null)
               // return;

             if ((hitRenderer.material.GetColor("_Color").a < transparencyLimit) || (hitRenderer.material.color.a < transparencyLimit))
            {
                lidarParticle.SetActive(false);
                belowTransparencyLimit = true;
            }
            /*else if ((hitRenderer.material.mainTexture != null) && (hitRenderer.material.mainTexture.isReadable))
            {
                Texture2D tex = hitRenderer.material.mainTexture as Texture2D;
                Vector2 pixelUV = hitInfo.textureCoord;
                pixelUV.x *= tex.width;
                pixelUV.y *= tex.height;
                Color colorOfPixel = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                float transparency = colorOfPixel.a;

                if (transparency < transparencyLimit)
                {
                    lidarParticle.SetActive(false);
                }
            }*/
            else {
                //lidarParticle.SetActive(true);
                belowTransparencyLimit = false;
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
            var lerp = 1f - ((1f / (1f + hit.distance)) * blendFactor); 
            return lerp;
        }
  }
}