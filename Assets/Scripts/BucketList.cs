using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BucketList : MonoBehaviour
{
    [Header("Bucket")]
    [Tooltip("Bucket")]
    public GameObject bucket;
    //Nur für UI Anzeige (Test)
    [Header("Checklist")]
    [Tooltip("Checklist Elements")]
    public TextMeshProUGUI[] textElement;
    public Image[] checkIcon;
    public Image errorIcon;
    public List<string> bucketListContent;
    public GameObject checkList;
    [Tooltip("Color on Error")]
    public Material red;
    public Material white;
    public float colorChangetimer = 1f;
    public float errorTimer = 5f;
    public float errorTimertotal = 5f;
    [Tooltip("Return Speed")]
    public float speed = 1f;
    public float fadingTime = 2f;
    public float beginingDelay = 1.0f;

    // Debugging
    public List<GameObject> _bucketList;

    // To change color by Coroutine Calls
    protected bool coroutineCalled = false;
    protected Collider bucketCollider;
    public GameObject[] allGameObjects;
    protected IEnumerator moveCoroutine;
    protected IEnumerator delayCoroutine;
    // Controller 
    protected Game_Manager controller = Game_Manager.Instance;

    public void Start()
    {
        //Objekte die im Eimer erkannt werden sollen einem Array zuweisen (in diesem Fall ALLE GameObjekte die aktiv in der Szene sind zur Demonstration).
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        FetchAllPositions();

        //Den Collider(MeshCollider) des Eimers einer Variable zuweisen.
        bucketCollider = GetComponent<Collider>();
        errorTimer = errorTimertotal;

        for(int i = 0; i < textElement.Length; i++)
        {
            textElement[i].text = bucketListContent[i];
            checkIcon[i].enabled = false;
        }
        errorIcon.enabled = false;
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

        //Nur für UI Anzeige (Test)
        //textElement.text = "Anzahl Objekte im Eimer" + "\n" + bucketList.Count.ToString();
        //checkIcon.text = ListToText(bucketList);

        _bucketList = controller.GetBucketObjects();
    }

    //Nur für UI Anzeige (Test)
    public string ListToText(List<GameObject> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += "-" + " " + listMember.name + "\n";
        }
        return result;
    }

    public void FetchAllPositions(){
        foreach(GameObject obj in allGameObjects){
            // Target objects
            controller.AddPositions(obj.GetHashCode(), obj.transform.position);               
        }
    }

    public void CheckGameObject(GameObject gameObj){       
            // Gameobject Tag und gelistete Tags müssen übereinstimmen
            if (bucketListContent.Contains(gameObj.tag))
            {
            
            //checkIcon[i].enabled = true;
            controller.AddToBucketList(gameObj);
            controller.ResetMaterial(gameObj);
                controller.AddPlayerScore();
                Debug.Log($"GameObject found {gameObj.tag}");
            }
            else if (!bucketListContent.Contains(gameObj.tag) && gameObj.tag != "Controller")
            {
                Debug.Log($"False object {gameObj.tag}");
                // Set Gameobject back to it's original position
                Vector3 position = controller.FindOriginalPos(gameObj);
                Debug.Log($"False object {position}");
                delayCoroutine= DelayAndMove(gameObj, gameObj.transform.position, position, speed);
                StartCoroutine(delayCoroutine);

                if (!coroutineCalled)
                {
                    errorIcon.enabled = true;
                    // Change color to red
                    StartCoroutine("FlashColor");
                    controller.ReducePlayerScore();
                }
                else
                {
                    // Set color back to white
                    checkList.GetComponent<Renderer>().material = white;
                }
            }       
    }

    public void CleanUp(){
        controller.CleanUp();
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
            checkList.GetComponent<Renderer>().material = red;
            yield return new WaitForSeconds(0.3f);
            checkList.GetComponent<Renderer>().material = white;
            yield return new WaitForSeconds(0.3f);
        }
        coroutineCalled = false;
    }

    private IEnumerator DelayAndMove(GameObject objectToMove, Vector3 a, Vector3 b, float speed)
    {
        yield return new WaitForSeconds(beginingDelay);
        // Make invisible
        objectToMove.GetComponent<Renderer>().enabled = false;
        objectToMove.GetComponent<Collider>().enabled = false;
        moveCoroutine = MoveFromTo(objectToMove, a, b, speed);
        // After the delay do..
        StartCoroutine(moveCoroutine); 
    }

    public IEnumerator MoveFromTo(GameObject objectToMove, Vector3 a, Vector3 b, float speed)
    {
        Debug.Log($"Move Object");
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        // Move out of bucket by finding the centre of bucket and then move up straight
        objectToMove.transform.position = new Vector3(
            this.GetComponent<Renderer>().bounds.center.x,
            this.GetComponent<Renderer>().bounds.center.y + 1.0f,
            this.GetComponent<Renderer>().bounds.center.z);
        // Move gradiently back 
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.transform.position = b;
        // Set visible
        objectToMove.GetComponent<Renderer>().enabled = true;
        objectToMove.GetComponent<Collider>().enabled = true;
    }
}
