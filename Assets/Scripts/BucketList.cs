using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BucketList : MonoBehaviour
{
    //Nur für UI Anzeige (Test)
    [Header("Checklist")]
    [Tooltip("Checklist Elements")]
    public TextMeshProUGUI[] textElement;
    public Image[] checkIcon;
    public Image errorIcon;
    public string[] listContent;
    public GameObject checkList;
    [Tooltip("Color on Error")]
    public Material red;
    public Material white;
    public float colorChangetimer = 1f;
    public float errorTimer = 5f;
    public float errorTimertotal = 5f;

    // Debugging
    public List<GameObject> _bucketList;
  

    // To change color by Coroutine Calls
    protected bool coroutineCalled = false;
    protected Collider bucketCollider;
    protected GameObject[] allGameObjects;
    protected bool errorBreak = false;
    protected bool colorchange = false;
    // Controller 
    Game_Manager controller = Game_Manager.Instance;

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
            textElement[i].text = listContent[i];
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
            if (bucketCollider.bounds.Contains(gameObj.transform.position))
            {
                if (gameObj != gameObject)
                {
                    for(int i = 0; i < listContent.Length; ++i){
                        // Gameobject Tag und gelistete Tags müssen übereinstimmen
                        if (!controller.GetBucketObjects().Contains(gameObj) && gameObj.tag == listContent[i])
                        {
                            checkIcon[i].enabled = true;
                            controller.ResetMaterial(gameObj);
                            controller.Add(gameObj);                            
                            controller.AddPlayerScore();
                            Debug.Log($"GameObject found {gameObj.tag}");
                        }
                        else{
                            // Set Gameobject back to it's original position
                            ResetPosition(gameObj);

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

        if (errorTimer <= 0)
        {
            errorBreak = false;
            colorchange = false;
            errorTimer = errorTimertotal;
        }

        if (errorBreak)
        {
            errorTimer -= 1 * Time.deltaTime;
            colorchange = true;
        }

        if(controller.GetBucketObjects().Count == listContent.Length){
            // Game over
            ResetPosition();
            //CleanUp();
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
            for(int i = 0; i < listContent.Length; i++){
                if (obj.tag == listContent[i]){
                    controller.AddPosition(obj, obj.transform.position);
                } else{
                    controller.Add(obj);
                }
                
            }           
        }
    }

    public void ResetPosition(GameObject obj)
    {
        Dictionary<GameObject, Vector3> pos = controller.GetPositions();
        foreach (KeyValuePair<GameObject, Vector3> entry in pos){
            if (entry.Key == obj)
            {
                Debug.Log("Reset the position");
                obj.transform.position = entry.Value;
            }
        }                   
    }

    public void ResetPosition(){
        List<GameObject> _bucketList = controller.GetBucketObjects();
        Dictionary<GameObject, Vector3> _originalPosition = controller.GetPosition();
        foreach(GameObject obj in _bucketList){

            foreach (KeyValuePair<GameObject, Vector3> entry in _originalPosition)
            {
                if(obj == entry.Key){
                    Debug.Log("Reset all the position");
                    obj.transform.position = entry.Value;
                }                
            }               
        }
    }

    public void CleanUp(){
        controller.CleanUp();
    }

    /// <summary>
    /// Adjust the color and flash up
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashColor()
    {
        while (colorchange && errorBreak)
        {
            coroutineCalled = true;
            checkList.GetComponent<Renderer>().material = red;
            yield return new WaitForSeconds(0.3f);
            checkList.GetComponent<Renderer>().material = white;
            yield return new WaitForSeconds(0.3f);
        }
        coroutineCalled = false;
        // set icon back
        errorIcon.enabled = false;
    }
}
