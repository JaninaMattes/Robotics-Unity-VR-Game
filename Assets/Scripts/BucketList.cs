﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BucketList : MonoBehaviour
{
    private Collider bucketCollider;
    private GameObject[] allGameObjects;
    private List<GameObject> bucketList = new List<GameObject>();

    //Nur für UI Anzeige (Test)
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI textUI2;

    void Start()
    {
        //Objekte die im Eimer erkannt werden sollen einem Array zuweisen (in diesem Fall ALLE GameObjekte die aktiv in der Szene sind zur Demonstration).
        allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        //Den Collider(MeshCollider) des Eimers einer Variable zuweisen.
        bucketCollider = GetComponent<Collider>();
    }

    void Update()
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
                    if (!bucketList.Contains(gameObj))
                    {
                        bucketList.Add(gameObj);
                    }
                }
            }

            else
            {
                if (bucketList.Contains(gameObj))
                {
                    bucketList.Remove(gameObj);
                }
            }
        }

        //Nur für UI Anzeige (Test)
        textUI.text = "Anzahl Objekte im Eimer" + "\n" + bucketList.Count.ToString();
        textUI2.text = ListToText(bucketList);
    }

    //Nur für UI Anzeige (Test)
    private string ListToText(List<GameObject> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += "-" + " " + listMember.name + "\n";
        }
        return result;
    }
}