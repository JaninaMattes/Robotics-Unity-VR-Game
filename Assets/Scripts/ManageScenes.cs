using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ManageScenes : MonoBehaviour
{
    public List<GameObject> objectstoBeMoved;
    private Scene[] scenes;
    public GameObject loadingElement;
    public Slider slider;
    public TextMeshProUGUI progressValue;
    public int levelInd;
    public bool loadLevel = false;
    public TextMeshProUGUI loadText;
    public string sceneLoadingName;

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
        StartCoroutine(LoadAsync(levelIndex));
    }

    IEnumerator LoadAsync (int sceneIndex)
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        loadingElement.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressValue.text = progress * 100f + "%";
            yield return null;
        }
        Invoke("DeactivateLoadingBar", 0.2f);
    }

    public void DeactivateLoadingBar()
    {
        loadingElement.SetActive(false);
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

        if (loadLevel)
        {
            loadText.text = "Loading" + " " + sceneLoadingName + "...";
            LoadLevelAdditive(levelInd);
            loadLevel = false;
        }
        
        if (scenes[scenes.Length - 1].isLoaded)
        {
            MoveObjectToScene(objectstoBeMoved, scenes[scenes.Length - 1]);
        }
    }
}
