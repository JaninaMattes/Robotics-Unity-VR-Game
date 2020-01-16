using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[ExecuteInEditMode]
public class ChangeMaterials : MonoBehaviour
{
    [Header("Snapdrop Zone Prefab")]
    public VRTK_SnapDropZone snapZone;
    [Header("Sensor Material")]
    [Tooltip("Sonar Materials")]
    public Material sonar_1_Material;
    public Material sonar_2_Material;
    [Tooltip("Radar Material")]
    public Material radar_1_Material;
    [Tooltip("Lidar Material")]
    public Material lidar_1_Material; // Wichtig Texturen
    [Tooltip("Floor Grid Orientation Material")]
    public Material gridorientation_Material;
    public string gridorientation_Tag;
    [Tooltip("Excluded Tag List")]
    public List<string> excludeTags = new List<string>();
   
    // Private Properties
    protected List<string> exclude = new List<string>();
    //protected GameObject[] currentGameObjects;
    protected Scene cur_Scene;

    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as 
        //this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone -= ObjectUnsnappedFromDropZone;
    }

    public void Start()
    {
        GetScene();
        exclude = excludeTags;
        // Kann beliebig erweitert werden
    }

    public void Update()
    {
        GetScene();
    }

    public void ResetMaterial(GameObject gameObject)
    {
        ResetMaterialFor(gameObject);
    }

    private void GetScene()
    {
        cur_Scene = SceneManager.GetActiveScene();
    }

    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        UpdateMaterial(snapZone.GetCurrentSnappedObject().tag);
    }

    protected virtual void ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
         
    }

    private void UpdateMaterial(string tag)
    {
        switch (tag)
        {
            case "SonarSensor_1":
                //Update Material
                UpdateMaterial(sonar_1_Material);
                break;
            case "SonarSensor_2":
                //Update Material
                UpdateMaterial(sonar_2_Material);
                break;
            case "LidarSensor":
                //Update Material
                UpdateMaterial(lidar_1_Material);
                break;
            case "RadarSensor":
                //Update Material
                UpdateMaterial(radar_1_Material);
                break;
            case "CameraSensor":
                //Revert Material
                ResetMaterial();
                break;
            default:
                //If no other case found
                ResetMaterial();
                break;
        }
    }

    private void UpdateMaterial(Material material)
    {
        Material[] m;
        //LightmapSettings.lightmaps = null;
        foreach (Renderer rend in controller.GetRenderer())
        {
          if (rend != null && !exclude.Contains(rend.tag)) //TODO: Über Layer definieren --> Belt/Patrone/Hände/Player/Guns/Bucketlist/Bucket etc
            {
                m = rend.materials;
                //Set grid orientation to floor
                if (rend.tag == gridorientation_Tag)
                {
                    //TODO: find Material that belongs to floor 
                    //then set to = gridorientation_Material
                }
                else
                {                 
                    //TODO: Check if Material is Water/Glas for SonarShader
                    for (int i = 0; i < m.Length; i++)
                    {
                        m[i] = material;
                    }
                    rend.materials = m;

                }               
            }  
        }
    }

    private void ResetMaterial()
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null)
            {
                rend.materials = controller.GetMaterial()[rend] as Material[];
            }
        }
    }

    private void ResetMaterialFor(GameObject gameObject)
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null)
            {
               rend.materials = controller.GetMaterial()[gameObject.GetComponent<Renderer>()] as Material[];
            }
        }
    }

    private void GetMaterials()
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null)
            {
                controller.GetMaterial().Add(rend, rend.materials);
            }
        }
    }

    private void GetMeshRenderer()
    {
        Renderer[] list = GameObject.FindObjectsOfType<Renderer>();
        controller.Set(list);
        GetMaterials();
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        //if (scene.buildIndex != 0)
        //{
            GetMeshRenderer();
        //}           
    }
}