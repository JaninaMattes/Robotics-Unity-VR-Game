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
    public Dictionary<int, GameObject> learnedObj { get; set; }


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
}
