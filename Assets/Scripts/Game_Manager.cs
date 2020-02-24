using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
using System.Linq;

/// <summary>
/// Singleton Design Pattern
/// </summary>
public class Game_Manager
{
    private static Game_Manager _Instance = null;
    // Game score
    private Game_Manager() { }

    /// <summary>
    /// Singleton Pattern to restrict instantiation
    /// </summary>
    public static Game_Manager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new Game_Manager();

            }
            return _Instance;
        }
    }

    protected int playerScore = 0;
    protected int playerHealth = 0;
    // Machine Learning Simulation
    protected List<GameObject> _bucketList = new List<GameObject>();
    //protected Dictionary<GameObject, Vector3> _originalPosition = new Dictionary<GameObject, Vector3>();
    protected Dictionary<int, Vector3> _originalPositions = new Dictionary<int, Vector3>();
    // Material Changer
    protected ReflectionProbe[] _reflectionProbes;
    protected Renderer[] _renderer;
    protected Hashtable _matList = new Hashtable();
    protected Material[] _allMaterials = new Material[5];
    protected Material gridorientation_Material;

    //public SonarLaser sonar1;
    protected SonarLaserAdv _sonar2;
    protected RadarLaser _radar;
    protected LiDar2 _lidar;
    protected LaserController _laser_controller;

    protected List<string> _exclude = new List<string>();
    protected string gridorientation_Tag;
    // Toggle light in the rooms per material
    protected GameObject[] _lightGameObjects = new GameObject[2];
    // Gun Objects
    protected string _patrone = "default";
    protected GameObject cameraScreen;
    protected Camera cameraRig;
    protected int originalCullingMask;

    protected Light pointLight;

    /// <summary>
    /// Gett and Setter  
    /// </summary>
    /// <param name="gridorientation_Tag"></param>
    public void SetGridOrientationTag(string gridorientation_Tag)
    {
        this.gridorientation_Tag = gridorientation_Tag;
    }

    public string GetGridOrientationTag()
    {
        return gridorientation_Tag;
    }

    public void SetGridOrientationMaterial(Material material)
    {
        this.gridorientation_Material = material;
    }

    public Material GetGridOrientationMaterial()
    {
        return gridorientation_Material;
    }

    public void SetExcludeTag(List<string> exclude)
    {
        _exclude = exclude;
    }
    public List<string> GetExcludeTag()
    {
        return _exclude;
    }

    public void SetSonar(SonarLaserAdv sonar)
    {
        _sonar2 = sonar;
    }
    public void SetRadar(RadarLaser radar)
    {
        _radar = radar;
    }
    public void SetLidar(LiDar2 lidar)
    {
        _lidar = lidar;
    }
    public void SetLaserController(LaserController laser_controller)
    {
        _laser_controller = laser_controller;
    }

    public SonarLaserAdv GetSonar()
    {
        return _sonar2;
    }
    public RadarLaser GetRadar()
    {
        return _radar;
    }
    public LiDar2 GetLidar()
    {
        return _lidar;
    }
    public LaserController GetLaserController()
    {
        return _laser_controller;
    }

    public void SetCamera(Camera cameraRig)
    {
        this.cameraRig = cameraRig;
    }

    public void SetOriginalMask(int cullingMask)
    {
        this.originalCullingMask = cullingMask;
    }

    public void Set(Dictionary<int, Vector3> _originalPositions)
    {
        this._originalPositions = _originalPositions;
    }
    public void Set(List<GameObject> _bucketList)
    {
        this._bucketList = _bucketList;
    }

    public void AddRenderer(Renderer[] _rend)
    {
        List<Renderer> cleanList = new List<Renderer>();
        //Filter for null vallues
        for (int i = 0; i < _rend.Length; i++) {
            cleanList.Add(_rend[i]);
            //Debug.Log("Add Renderer " + _rend[i]);
        }
        cleanList.RemoveAll(x => x == null);
        this._renderer = _renderer.Concat(cleanList.ToArray()).ToArray<Renderer>();
    }

    public void SetRenderer(Renderer[] _rend)
    {
        List<Renderer> cleanList = new List<Renderer>();
        //Filter for null vallues
        for (int i = 0; i < _rend.Length; i++)
        {
            cleanList.Add(_rend[i]);
            //Debug.Log("Add Renderer " + _rend[i]);
        }
        cleanList.RemoveAll(x => x == null);
        this._renderer = cleanList.ToArray();
    }

    public void SetMaterials(Renderer[] renderer)
    {
        this._matList = new Hashtable();

        foreach (Renderer rend in renderer)
        {
            if (rend != null)
            {
                this._matList.Add(rend, rend.materials);
            }
        }
    }

    /// <summary>
    /// Set all materials that come grom the Change Materials Script.
    /// </summary>
    /// <param name="_allMaterial"></param>
    public void SetAllMaterials(Material[] _allMaterial)
    {
        this._allMaterials = _allMaterial;
    }

    public Material[] GetAllMaterials()
    {
        return this._allMaterials;
    }

    public void AddToBucketList(GameObject _bucketList)
    {
        this._bucketList.Add(_bucketList);
    }

    public void Remove(GameObject _bucketList)
    {
        this._bucketList.Remove(_bucketList);
    }

    public List<GameObject> GetBucketObjects()
    {
        return this._bucketList;
    }

    public Renderer[] GetRenderer()
    {
        return this._renderer;
    }

    public Hashtable GetMaterial()
    {
        return this._matList;
    }

    public void SetPlayerHealth(int playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    public void AddPlayerHealth()
    {
        ++this.playerHealth;
    }

    public void ReducePlayerHealth()
    {
        --this.playerHealth;
    }

    public int GetPlayerHealth()
    {
        return this.playerHealth;
    }
    public void SetPlayerScore(int playerScore)
    {
        this.playerScore = playerScore;
    }

    public void AddPlayerScore(int score)
    {
        this.playerScore += score;
    }

    public void ReducePlayerScore(int score)
    {
        this.playerScore -= score;
    }

    public int GetPlayerScore()
    {
        return this.playerScore;
    }

    public void SetLights(GameObject[] lights)
    {
        _lightGameObjects = lights;
    }

    public GameObject[] GetLights()
    {
        return _lightGameObjects;
    }

    public void SetCameraScreen(GameObject cameraScreen)
    {
        this.cameraScreen = cameraScreen;
    }

    public GameObject GetCameraScreen()
    {
        return this.cameraScreen;
    }

    public void FindProbes()
    {
        _reflectionProbes = GameObject.FindObjectsOfType<ReflectionProbe>();
    }

    public ReflectionProbe[] GetProbes()
    {
        return this._reflectionProbes;
    }

    public void ToggleProbes(bool isOn){
        foreach(ReflectionProbe probe in _reflectionProbes){
           var cullMask =  probe.cullingMask;
           probe.cullingMask = cullMask | (1 << 11); // To make Layer 11 visible      
        }
    }

    public void AddPositions(int hashCode, Vector3 position)
    {
        if (!_originalPositions.ContainsKey(hashCode))
        {
            this._originalPositions.Add(hashCode, position);
        }
    }
    
    public Dictionary<int, Vector3> GetPositions()
    {
        return this._originalPositions;
    }

    public Vector3 FindOriginalPos(GameObject obj)
    {

        Vector3 position = new Vector3();
        foreach (KeyValuePair<int, Vector3> entry in _originalPositions)
        {
            if (obj.GetHashCode() == entry.Key)
            {
                position = entry.Value;
            }
        }
        return position;
    }

    public void GetMeshRenderer()
    {
        Renderer[] list = GameObject.FindObjectsOfType<Renderer>();
        SetRenderer(list);
    }

    public void ResetMaterial(GameObject obj)
    {
        Renderer m_ObjectRenderer = obj.GetComponent<Renderer>();

        foreach (Renderer rend in _renderer)
        {
            if (rend != null && rend == m_ObjectRenderer)
            {
                rend.materials = _matList[rend] as Material[];
            }
        }
    }

    public void ResetMaterial()
    {
        foreach (Renderer rend in GetRenderer())
        {
            if (rend != null)
            {
                rend.materials = GetMaterial()[rend] as Material[];
            }
        }
    }

    /// <summary>
    /// Function that contains a switch case 
    /// to toggle the right renderer and material
    /// per sensor used 
    /// </summary>
    /// <param name="tag"></param>
    public void UpdateMaterial(string tag)
    {
        Debug.Log("Update material" + tag);
        switch (tag)
        {
            case "SonarSensor_1":
                //Update Material
                UpdateMaterial(_allMaterials[0]);
                ActivateAllRenderer();
                SetLaserScript(tag);
                ToggleProbes(false);
                ResetCameraPixelScript();
                break;
            case "SonarSensor_2":
                //Update Material
                UpdateMaterial(_allMaterials[1]);
                ActivateAllRenderer();
                SetLaserScript(tag);
                ToggleProbes(false);
                ResetCameraPixelScript();
                break;
            case "LidarSensor":
                //Update Material
                UpdateMaterial(_allMaterials[3]);
                //DeactivateAllRenderer();
                SetLidarScript();
                ToggleProbes(false);
                ResetCameraPixelScript();
                break;
            case "RadarSensor":
                //Update Material
                UpdateMaterial(_allMaterials[2]);
                ActivateAllRenderer();
                SetLaserScript(tag);
                ToggleProbes(false);
                ResetCameraPixelScript();
                break;
            case "CameraSensor":
                //Revert Material
                ResetMaterial();
                ActivateAllRenderer();
                SetCameraPixelScript();                
                break;
            case "default":
                //If no other case found
                UpdateMaterial(_allMaterials[4]);
                ActivateAllRenderer();
                ToggleProbes(false);
                ResetCameraPixelScript();
                break;
            default:
                //If no other case found
                UpdateMaterial(_allMaterials[4]);
                ActivateAllRenderer();
                ToggleProbes(false);
                ResetCameraPixelScript();
                break;
        }
    }

    public void SetSnappedPatrone(string patrone)
    {
        _patrone = patrone;
    }

    public string GetSnappedPatrone()
    {
        return this._patrone;
    }

    public void ActivateAllRenderer()
    {
        foreach (Renderer rend in GetRenderer())
        {
            if (rend != null && rend.tag != "LineRenderer")
            {
                rend.enabled = true;
            }
        }
    }

    public void DeactivateAllRenderer()
    {
        foreach (Renderer rend in GetRenderer())
        {
            if (rend != null && !_exclude.Contains(rend.tag))
            {
                rend.enabled = false;
            }
        }
    }

    public void UpdateMaterial(Material material)
    {
        Material[] m;
        //LightmapSettings.lightmaps = null;
        foreach (Renderer rend in GetRenderer())
        {
            //Debug.Log(rend.ToString());
            if (rend != null && !_exclude.Contains(rend.tag))
            {
                //TODO: Über auch über Layer definieren 
                // --> Belt/Patrone/Hände/Player/Guns/Bucketlist/Bucket etc                
                m = rend.materials;
                //Set grid orientation to floor
                if (rend.tag == gridorientation_Tag)
                {
                    rend.material = gridorientation_Material;
                }
                else
                {
                    for (int i = 0; i < m.Length; i++)
                    {
                        m[i] = material;
                    }
                    rend.materials = m;
                }
            }
            else
            {
                Debug.Log("Excluded " + rend.tag);
            }
        }
    }


    public void SetLaserScript(string sensor)
    {
        _laser_controller.enabled = true;
        // Lidar
        _lidar.lidarActive = false;

        if (sensor == "SonarSensor_1")
        {
            // Sonar 
            //sonar1.enabled = true;
            //sonar1.Material = sonar_1_Material;
        }
        else if (sensor == "SonarSensor_2")
        {
            // Sonar 
            _sonar2.enabled = true;
            _sonar2.material = _allMaterials[1];
            _radar.enabled = false;
            _laser_controller.sonarHits = new Vector4[50];
        }
        else if (sensor == "RadarSensor")
        {
            // Radar
            _radar.enabled = true;
            _radar.material = _allMaterials[2];
            _sonar2.enabled = false;
            _laser_controller.sonarHits = new Vector4[50];
        }
        else { }
    }

    public void SetLidarScript()
    {
        // Laser
        //sonar1.enabled = false;
        _sonar2.enabled = false;
        _radar.enabled = false;
        _laser_controller.enabled = false;
        // Lidar
        _lidar.lidarActive = true;
    }

    public void SetLight(int level)
    {
        Debug.Log("Set Light" + level);
        switch (level)
        {
            case 2:
                _lightGameObjects = GameObject.FindGameObjectsWithTag("ForestLight");
                break;
            case 3:
                _lightGameObjects = GameObject.FindGameObjectsWithTag("KitchenLight");
                Debug.Log(_lightGameObjects[0]);
                Debug.Log(_lightGameObjects[1]);
                break;
            default:
                break;
        }
    }

    public void ToggleLight(int level, bool active)
    {
        Debug.Log("Switch Light " + active);
        switch (level)
        {
            case 2:
                _lightGameObjects[0].SetActive(active);
                _lightGameObjects[1].SetActive(active);
                break;
            case 3:
                _lightGameObjects[0].SetActive(active);
                _lightGameObjects[1].SetActive(active);
                _lightGameObjects[2].SetActive(active);
                _lightGameObjects[3].SetActive(active);
                _lightGameObjects[4].SetActive(active);
                break;
            default:
                break;
        }
    }
    public void SetPointLight(Light pointLight)
    {
        this.pointLight = pointLight;
    }

    public void SetCameraPixelScript()
    {
        if (pointLight != null)
        {
            pointLight.cullingMask = -1;
        }
        cameraScreen.SetActive(true);
        cameraRig.clearFlags = CameraClearFlags.SolidColor;
        cameraRig.backgroundColor = Color.black;
        cameraRig.cullingMask = (1 << 11) | (1 << 13); // To set Layermask to Layer 11     Alternativ 1 << LayerMask.NameToLayer("Gun")  oder 2048 (2 hoch 11 -> entspricht Integerwert des Layers)
    }

    public void ResetCameraPixelScript()
    {
        if (pointLight != null)
        {
            pointLight.cullingMask = (1 << 11) | (1 << 12) | (1 << 13);
        }
        cameraScreen.SetActive(false);
        cameraRig.clearFlags = CameraClearFlags.Skybox;
        //cameraRig.cullingMask = this.originalCullingMask;   
        cameraRig.cullingMask = -1; // -1 = Everything 
    }


    public void CleanUp()
    {
        Dictionary<int, Vector3> positions = null;
        List<GameObject> list = null;
        _bucketList = list;
        _originalPositions = positions;
    }

    /// <summary>
    /// Interactions with Player controller
    /// 0 = right vibration
    /// 1= left vibration
    /// 2 = both
    /// </summary>
    /// <param name="i"></param>
    /// <param name="AC"></param>
    /**
    public void vibrateController(int i, AudioClip AC){
         switch (i){
             case 0:
                 //outputs vibration to right controller
                 OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(AC));
             break;
             case 1:
                 //outputs vibration to left controller
                 OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(AC));
                 break;
             case 2:
                 //outputs vibration to left controller
                 OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(AC));
                 OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(AC));
                 break;
         }
        
     }
    */


}