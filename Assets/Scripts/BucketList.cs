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

    public Color red;
    public Color white;
    public float colorChangetimer = 1f;
    public float errorTimer = 5f;
    public float errorTimertotal = 5f;
  

    // To change color by Coroutine Calls
    protected bool coroutineCalled = false;
    protected Collider bucketCollider;
    protected GameObject[] allGameObjects;
    protected bool damagebreak = false;
    protected bool colorchange = false;
    // Controller 
    Game_Manager controller = Game_Manager.Instance;

    public void Start()
    {
        //Objekte die im Eimer erkannt werden sollen einem Array zuweisen (in diesem Fall ALLE GameObjekte die aktiv in der Szene sind zur Demonstration).
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

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
                            controller.Add(gameObj);
                            checkIcon[i].enabled = true;
                        }
                        else{
                            if (!coroutineCalled)
                            {
                                errorIcon.enabled = true;
                                // Change color to red
                                StartCoroutine("FlashColor");
                            }
                            else
                            {
                                // Set color back to white
                                GetComponent<SpriteRenderer>().color = white;
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
            damagebreak = false;
            colorchange = false;
            errorTimer = errorTimertotal;
        }

        if (damagebreak)
        {
            errorTimer -= 1 * Time.deltaTime;
            colorchange = true;
        }

        //Nur für UI Anzeige (Test)
        //textElement.text = "Anzahl Objekte im Eimer" + "\n" + bucketList.Count.ToString();
        //checkIcon.text = ListToText(bucketList);
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

    /// <summary>
    /// Adjust the color and flash up
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashColor()
    {
        while (colorchange && damagebreak)
        {
            coroutineCalled = true;
            GetComponent<SpriteRenderer>().color = red;
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = white;
            yield return new WaitForSeconds(0.3f);
        }
        coroutineCalled = false;
        // set icon back
        errorIcon.enabled = false;
    }
}
