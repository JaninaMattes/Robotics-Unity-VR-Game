using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTag : MonoBehaviour
{

    public string newTag;


    // Start is called before the first frame update
    void OnEnable()
    {
        
        gameObject.tag = newTag;

        //for (int i = 0; i < gameObject.transform.childCount; i++)
        //{
        //    gameObject.transform.GetChild(i).tag = newTag;
        //}

        foreach (Transform child in transform)
        {
            child.tag = newTag;
            //child is your child transform
        }

    }


}
