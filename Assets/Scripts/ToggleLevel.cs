using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;
using UnityEngine.UI;
using TMPro;

public class ToggleLevel : MonoBehaviour
{
    [Header("Snapdrop Zone Prefab")]
    public VRTK_SnapDropZone snapZone;
    [Tooltip("Level Index and Information")]
    public int WorkshopLevelIndex;
    public int LevelIndex;
    public GameObject lidarGrid;
    public GameObject player;
    public VRTK_InteractableObject headSet;
    private GameObject[] headsetsInScene;
    private Color highlightColor;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI textProgress;
    public TextMeshProUGUI textLevel;
    public string levelNameMinigame;
    public string levelNameWorkshop;

    void Awake()
    {
        if (player != null) { DontDestroyOnLoad(player); }
        if (lidarGrid != null) { DontDestroyOnLoad(lidarGrid); }
    }

    void Start()
    {
        highlightColor = snapZone.highlightColor;
    }

    void Update()
    {
        headsetsInScene = GameObject.FindGameObjectsWithTag("Headset");
        if (headsetsInScene.Length > 1)
        {
            Destroy(headsetsInScene[1]);
        }
    }

    public void OnEnable()
    {
        Debug.Log("### START ####");
        snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
        snapZone.ObjectExitedSnapDropZone += ObjectExitedSnapDropZone;
        snapZone.ObjectEnteredSnapDropZone += OnObjectEnteredSnapDropZone;
        headSet.InteractableObjectGrabbed += InteractableObjectGrabbed;
        headSet.InteractableObjectUngrabbed += InteractableObjectUngrabbed;
    }

    public void OnDisable()
    {
        snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone -= ObjectUnsnappedFromDropZone;
        snapZone.ObjectExitedSnapDropZone -= ObjectExitedSnapDropZone;
        snapZone.ObjectEnteredSnapDropZone -= OnObjectEnteredSnapDropZone;
        headSet.InteractableObjectGrabbed -= InteractableObjectGrabbed;
        headSet.InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
    }

    public void LoadLevel(int sceneIndex, string levelName)
    {
        StartCoroutine(LoadAsynchron(sceneIndex, levelName));
    }

    IEnumerator LoadAsynchron(int sceneIndex, string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        textLevel.text = "Loading" + " " + levelName + "...";
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            textProgress.text = progress * 100f + "%";

            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    // Eventhandler
    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        snapZone.highlightAlwaysActive = true;
    }

    protected virtual void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        snapZone.highlightAlwaysActive = false;
    }

    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        if (SceneManager.GetActiveScene().buildIndex != LevelIndex)
        {
            LoadLevel(LevelIndex, levelNameMinigame);
        }
    }

    protected virtual void ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {

    }

    protected virtual void ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        snapZone.highlightColor = new Color(snapZone.highlightColor.r, snapZone.highlightColor.g, snapZone.highlightColor.b, highlightColor.a);
        if (SceneManager.GetActiveScene().buildIndex != WorkshopLevelIndex)
        {
            LoadLevel(WorkshopLevelIndex, levelNameWorkshop);
        }
    }

    protected virtual void OnObjectEnteredSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        snapZone.highlightColor = new Color(snapZone.highlightColor.r, snapZone.highlightColor.g, snapZone.highlightColor.b, 0.8f);
    }

}