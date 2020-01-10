using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[ExecuteInEditMode]
public class ChangeMaterial : MonoBehaviour
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

    protected Renderer[] renderer;
    protected GameObject[] currentGameObjects;
    protected GameObject currentSnappedObject = null;
    protected Scene cur_Scene;
    protected int pas_sceneIndex = 0;
    protected string snapped_Tag;
    protected bool isSnapped = false;

    void Start()
    {
        GetScene();
        pas_sceneIndex = cur_Scene.buildIndex;
        renderer = (MeshRenderer[])Object.FindObjectsOfType(typeof(MeshRenderer));
    }
    
    void Update()
    {
        GetScene();
        GetSnappedObj();
         
        if (cur_Scene.buildIndex != 0 && cur_Scene.buildIndex != pas_sceneIndex)
        {
            currentGameObjects = cur_Scene.GetRootGameObjects();
        }
        pas_sceneIndex = cur_Scene.buildIndex;

        if (isSnapped)
        {
            // update the materials per Level
            UpdateMaterial();
        }        
    }

    public void GetScene()
    {
        cur_Scene = SceneManager.GetActiveScene();        
    }

    public void GetSnappedObj()
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

    private void UpdateMaterial()
    {
        snapped_Tag = snapZone.GetCurrentSnappedObject().tag;

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
                //Revert Material .... 
                break;
            default:
                //
                break;
        }
    }

    private void UpdateMaterial(Material material)
    {
        //foreach (renderer rend in renderer)
        //{
        //    rend.material = material;
        //}
        
        foreach (GameObject obj in currentGameObjects)
        {
           obj.transform.GetComponent<Renderer>().material = material;
        }
    }
}

