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
    protected GameObject currentSnappedObject = null;
    protected Scene cur_Scene;
    protected string snapped_Tag = null;
    protected string comp_Tag = null;
    protected bool isSnapped = false;

    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as 
        //this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
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
        GetSnappedObj();

        if (isSnapped) //&& cur_Scene.buildIndex != 0)
        {
            snapped_Tag = currentSnappedObject.tag;
                if (snapped_Tag != comp_Tag)
            {
                // update the materials per Level
                UpdateMaterial(snapped_Tag);
            }
            comp_Tag = snapped_Tag;
        }
    }

    public void ResetMaterial(GameObject gameObject)
    {
        ResetMaterialFor(gameObject);
    }

    private void GetScene()
    {
        cur_Scene = SceneManager.GetActiveScene();
    }

    private void GetSnappedObj()
    {
        if (snapZone.GetCurrentSnappedObject() != null)
        {
            currentSnappedObject = snapZone.GetCurrentSnappedObject();
            isSnapped = true;
        }
        else
        {
            isSnapped = false;
        }
    }

    private void UpdateMaterial(string tag)
    {
        switch (snapped_Tag)
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
        //LightmapSettings.lightmaps = null;
        foreach (Renderer rend in controller.GetRenderer())
        {
          if (rend != null && !exclude.Contains(rend.tag)) //TODO: Über Layer definieren --> Belt/Patrone/Hände/Player/Guns/Bucketlist/Bucket etc
            {
                Material[] m = rend.materials;
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
                        rend.materials = m;
                    }

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