using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public List<GameObject> objectstoBeMoved;
    private Scene[] scenes;

    void Awake()
    {     
        objectstoBeMoved.Add(gameObject);
    }

    public void MoveObjectToScene(List<GameObject> g, Scene s)
    {
        for(int i = 0; i < g.Count; i++)
        {
            g[i].transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(g[i], s);
        }
    }

    public void LoadLevelAdditive(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
    }

    public void UnLoadLevel(Scene sc)
    {
        SceneManager.UnloadSceneAsync(sc);
    }

    void Update()
    {
        scenes = new Scene[SceneManager.sceneCount];
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i);
            if(i < (scenes.Length - 1))
            {
              if (scenes[i].isLoaded)
               {
                    UnLoadLevel(scenes[i]);
               }
            }
        }

        if (scenes[scenes.Length - 1].isLoaded)
        {
            MoveObjectToScene(objectstoBeMoved, scenes[scenes.Length - 1]);
        }
    }
}
