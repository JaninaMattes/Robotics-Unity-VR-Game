using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private static Game_Manager _Instance = null;
    // Game score
    public int playerScore { get; set; }
    public int playerHealth { get; set; }
    // Machine Learning Simulation
    protected Dictionary<int, GameObject> learnedObj = new Dictionary<int, GameObject>();
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

    public void Set(Dictionary<int, GameObject> learnedObj)
    {
        this.learnedObj = learnedObj;
    }

    public void Set(Renderer[] _renderer)
    {
        this._renderer = _renderer;
    }

    public void Set(Hashtable _matList)
    {
        this._matList = _matList;
    }

    public Dictionary<int, GameObject> GetGameObjectd()
    {
        return this.learnedObj;
    }

    public Renderer[] GetRenderer()
    {
        return this._renderer;
    }

    public Hashtable GetMaterial()
    {
        return this._matList;
    }
}
