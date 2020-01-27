using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Munition : MonoBehaviour
{
    [Range(0, 100)]
    public int maxEnergyLoad = 100;

    public int energyAmount {
        get { return maxEnergyLoad; }
        set {
            maxEnergyLoad = value; 
            SetEnergyBalken(maxEnergyLoad); 
        }
    }

    GameObject energyObject;

    // Start is called before the first frame update
    void Start()
    {
        energyObject = transform.GetChild(0).gameObject;
    }

    void SetEnergyBalken(int energyAmount_)
    {
        energyObject.transform.localScale = new Vector3(1,1, (float)energyAmount_ / 100);
    }
}
