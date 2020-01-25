using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using VRTK;

public class BucketList : MonoBehaviour
{
    [Header("Bucket")]
    [Tooltip("Bucket")]
    public GameObject bucket;
    public List<string> bucketListContent;
    public GameObject checkList;
    [Tooltip("Color on Error")]
    public float colorChangetimer = 1f;
    public float errorTimer = 5f;
    public float errorTimertotal = 5f;
    [Tooltip("Return Speed")]
    public float speed = 1f;
    public float fadingTime = 2f;

    [Header("Bucket Check - Excluded Tags")]
    public List<string> excludeTags = new List<string>();

    [Header("UI Checklist")]
    public Image UIDefault;
    public TextMeshProUGUI checkListHeader;
    public List<GameObject> checkedObjects= new List<GameObject>();
    private int textCounter = 0;
    public Color defaultColor;
    public Color errorColor;
    public string invalidText;
    public string checkListHeaderText;
    public VerticalLayoutGroup backgroundTransparent;
    public VerticalLayoutGroup backgroundDefault;

    protected bool coroutineCalled = false;
    protected Collider bucketCollider;
    public GameObject[] allGameObjects;
    protected IEnumerator moveCoroutine;
    // Controller 
    protected Game_Manager controller = Game_Manager.Instance;

    public void Start()
    {
        //Objekte die im Eimer erkannt werden sollen einem Array zuweisen (in diesem Fall ALLE GameObjekte die aktiv in der Szene sind zur Demonstration).
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        //FetchAllPositions();

        //Den Collider(MeshCollider) des Eimers einer Variable zuweisen.
        bucketCollider = GetComponent<Collider>();
        errorTimer = errorTimertotal;
    }

    public void Update()
    {
        //Überprüfung aller GameObjekte im Array.
        //Befindet sich die Postion(in Unity immer der Mittelpunkt der geometrischen Form) eines GameObjekts innerhalb der Collider-Grenzen (collider.bounds) und das GameObjekt ist nicht der Eimer selbst(der Eimer befindet sich natürlich immer in den eigenen Collidergrenzen),
        // ...so wird dieses Objekt der Bucketliste hinzugefügt, falls es nicht schon in dieser vorhanden ist (Vermeidung von Redundanz).
        //Ist dies nicht der Fall, wird das Objekt wieder aus der BucketListe gelöscht.

        //Die Überprüfung funktioniert auch mit renderer.bounds, falls man es nicht über den Collider abfragen möchte oder der Eimer keinen Collider hätte.
        //Bei einer Box könnte der Collider bspw. die Objekte vom Hinzufügen blockieren, da keine Objekte durch einen Collider hindurch in einen Eimer geworfen werden können.
        //Da hier der Meshcollider aber direkt um das Eimerobjekt, um den Mesh, anliegt, ist das kein Hinderniss.

        foreach (GameObject gameObj in allGameObjects)
        {
            Vector3 position = gameObj.transform.position;

            if (bucketCollider.bounds.Contains(position))
            {
                if (gameObj != bucket && !controller.GetBucketObjects().Contains(gameObj))
                {                    
                    CheckGameObject(gameObj);                                         
               }
            }

            else
            {
                if (controller.GetBucketObjects().Contains(gameObj))
                {
                    controller.Remove(gameObj);
                }
            }
        }

    }

    public void FetchAllPositions(){
        foreach(GameObject obj in allGameObjects){
        // Target objects
        controller.AddPositions(obj);               
        }
    }

    public void CheckGameObject(GameObject gameObj){       
            // Gameobject Tag und gelistete Tags müssen übereinstimmen
            if (bucketListContent.Contains(gameObj.tag))
            {
            gameObj.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
            controller.AddToBucketList(gameObj);
            controller.ResetMaterial(gameObj);
            controller.AddPlayerScore();
            Debug.Log($"GameObject found {gameObj.tag}");
            SetDefaultUIText(gameObj);

            }
            else
            {
            if (!excludeTags.Contains(gameObj.tag)) { 
                Debug.Log($"False object {gameObj.tag}");
                // Set Gameobject back to it's original position
                Vector3 position = controller.FindOriginalPos(gameObj);
                MoveGameObjectTo(gameObj.transform, gameObj.transform.position, position, speed);
                if (!coroutineCalled)
                {
                    DisableDefaultUI();
                    StartCoroutine("FlashColor");
                    controller.ReducePlayerScore();
                }
                }
                else
                {
                    // Set color back to white
                    //checkList.GetComponent<Renderer>().material = white;
                }
            }
    }

    public void CleanUp(){
        controller.CleanUp();
    }

    public void MoveGameObjectTo(Transform objectToMove, Vector3 a, Vector3 b, float speed){
        moveCoroutine = MoveFromTo(objectToMove, a, b, speed);
        StartCoroutine(moveCoroutine);
    }

    /// <summary>
    /// Adjust the color and flash ups
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashColor()
    {
        float step = (fadingTime / 0.5f) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            coroutineCalled = true;
            t += step;
            coroutineCalled = true;
            EnableErrorUI();
            yield return new WaitForSeconds(0.3f);
            DisableErrorUI();
            yield return new WaitForSeconds(0.3f);
        }
        DisableErrorUI();
        EnableDefaultUI();
        coroutineCalled = false;
    }

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    {
        Debug.Log($"Move Object");
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
    }

    /// <summary>
    /// UI Canvas
    /// </summary>
    // UI Checklist activation / deactivation & Itemtext setup

    private void EnableErrorUI()
    {
        UIDefault.color = errorColor;
        UIDefault.enabled = true;
        checkListHeader.text = invalidText;
        checkListHeader.enabled = true;
    }

    private void DisableErrorUI()
    {
        UIDefault.enabled = false;
        checkListHeader.enabled = false;
    }

    private void  SetDefaultUIText(GameObject gameObj)
    {
        checkedObjects[textCounter].GetComponent<TextMeshProUGUI>().text = gameObj.name.ToString();
        checkedObjects[textCounter].SetActive(true);
        ForceCanvasUpdate();
        textCounter++;
    }

    private void EnableDefaultUI()
    {
        checkListHeader.text = checkListHeaderText;
        UIDefault.color = defaultColor;
        UIDefault.enabled = true;
        checkListHeader.enabled = true;
        for (int i = 0; i < textCounter; i++)
        {

            checkedObjects[i].SetActive(true);
        }
        ForceCanvasUpdate();
    }

    private void DisableDefaultUI()
    {
        foreach (GameObject checkedObject in checkedObjects) { 

            checkedObject.SetActive(false);
    }  

    }

    private void ForceCanvasUpdate()
    {
        Canvas.ForceUpdateCanvases();
        backgroundTransparent.enabled = false;
        backgroundDefault.enabled = false;
        backgroundTransparent.enabled = true;
        backgroundDefault.enabled = true;
    }

}
