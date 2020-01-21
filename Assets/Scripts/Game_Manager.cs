using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private static Game_Manager _Instance = null;
    // Game score
    protected int playerScore = 0;
    protected int playerHealth = 0;
    // Machine Learning Simulation
    protected List<GameObject> _bucketList = new List<GameObject>();
    protected Dictionary<GameObject, Vector3> _originalPosition = new Dictionary<GameObject, Vector3>();
    protected Dictionary<GameObject, Vector3> _originalPositions = new Dictionary<GameObject, Vector3>();
    // Material Changer
    protected Renderer[] _renderer;
    protected Hashtable _matList = new Hashtable();

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

    public void Set(Dictionary<GameObject, Vector3> _originalPosition)
    {
        this._originalPosition = _originalPosition;
    }
    public void Set(List<GameObject> _bucketList)
    {
        this._bucketList = _bucketList;
    }

    public void Set(Renderer[] _renderer)
    {
        this._renderer = _renderer;
    }

    public void Set(Hashtable _matList)
    {
        this._matList = _matList;
    }

    public void AddToBucketList(GameObject _bucketList)
    {
        this._bucketList.Add(_bucketList);
    }

    public void Remove(GameObject _bucketList){
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

    public void SetPlayerHealth(int playerHealth){
        this.playerHealth = playerHealth;
    }

    public void AddPlayerHealth(){
        ++ this.playerHealth;
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

    public void AddPlayerScore()
    {
        ++this.playerScore;
    }


    public void ReducePlayerScore()
    {
        --this.playerScore;
    }


    public int GetPlayerScore()
    {
        return this.playerScore;
    }

    public void AddPosition(GameObject obj, Vector3 pos)
    {
        if (!_originalPosition.ContainsKey(obj)){
            this._originalPosition.Add(obj, pos);
        }            
    }

    public Dictionary<GameObject, Vector3> GetPosition( )
    {
        return this._originalPosition;
    }

    public void AddPositions(GameObject obj)
    {
        if (!_originalPosition.ContainsKey(obj))
        {
            this._originalPositions.Add(obj, obj.transform.position);
        }        
    }

    public Dictionary<GameObject, Vector3> GetPositions()
    {
        return this._originalPositions;
    }
    public void GetMeshRenderer()
    {
        Renderer[] list = GameObject.FindObjectsOfType<Renderer>();
        Set(list);
        GetMaterial();
    }

    public void CleanUp(){
        Dictionary<GameObject, Vector3> position = null;
        Dictionary<GameObject, Vector3> positions = null;
        List <GameObject> list = null;
        _bucketList = list;
        _originalPosition = position;
        _originalPositions = positions;
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
}
