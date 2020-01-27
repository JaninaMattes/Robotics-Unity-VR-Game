using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class Lasergun : MonoBehaviour
{

    public GameObject laserBlastPrefab;

    public Transform laserGunOrigin;

    public int projectileSpeed = 200;

    public GameObject explosionPrefab;

    VRTK_InteractableObject interactableGun;

    public VRTK_SnapDropZone snapDropZone;

    public int laserEnergyDrain = 10;

    public float laserReloadTimeInSec = 1f;

    Energy_Munition energyMunitionScript;

    int energyAmount = 0;

    public Text energyText;

    public RectTransform energyProgressBar;

    public Image laserReloadProgressBar;

    public Text laserReloadProgresText;

    public AudioSource laserGunPewSound;

    public AudioSource energyEmptySound;

    bool laserReloaded = true;

    MeshCollider meshCollider;


    private void Start()
    {
        EnergyUI();
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    private void OnEnable()
    {
        interactableGun = (interactableGun == null ? GetComponent<VRTK_InteractableObject>() : interactableGun);

        if (interactableGun != null)
        {
            interactableGun.InteractableObjectUsed += InteractableObjectUsed;
            interactableGun.InteractableObjectUnused += InteractableObjectUnused;
        }
        if (snapDropZone != null)
        {
            snapDropZone.ObjectSnappedToDropZone += SnapDropZone_ObjectSnappedToDropZone;
            snapDropZone.ObjectUnsnappedFromDropZone += SnapDropZone_ObjectUnsnappedFromDropZone;
        }
    }

    void SnapDropZone_ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        energyAmount = 0;
        energyMunitionScript = null;
        EnergyUI();

    }


    void SnapDropZone_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        energyMunitionScript = e.snappedObject.GetComponent<Energy_Munition>();
        energyAmount = energyMunitionScript.energyAmount;
        ChangeEnergyAmount(0);
    }


    protected virtual void OnDisable()
    {
        if (interactableGun != null)
        {
            interactableGun.InteractableObjectUsed -= InteractableObjectUsed;
            interactableGun.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }

    private void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (energyMunitionScript != null && energyAmount >= laserEnergyDrain && laserReloaded) {
            Shoot();
            ChangeEnergyAmount(laserEnergyDrain);
            laserReloaded = false;
            StartCoroutine(Reload(laserReloadTimeInSec));
        }
        else
        {
            energyEmptySound.Play();
        }
    }

    private void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
    }


    void Shoot()
    {
        laserGunPewSound.Play();
        GameObject blast = Instantiate(laserBlastPrefab, laserGunOrigin.position, gameObject.transform.rotation);
        LaserProjectile projectileScript = blast.GetComponent<LaserProjectile>();
        projectileScript.speed = projectileSpeed;
        projectileScript.explosionPrefab = explosionPrefab;
    }

    void ChangeEnergyAmount(int energyChange)
    {
        energyAmount -= energyChange;
        energyMunitionScript.energyAmount -= energyChange;
        EnergyUI();
        //Debug.Log("Remaining Energy " + energyAmount);
    }

    void EnergyUI()
    {
        energyText.text = energyAmount.ToString();
        energyProgressBar.sizeDelta = new Vector2(((float)energyAmount / 100f)*800f, energyProgressBar.sizeDelta.y);
    }

    IEnumerator Reload(float reloadTime)
    {
        float counter = 0;
        while (counter <= reloadTime)
        {
            counter += Time.deltaTime;

            float normalizedCounter = Mathf.Clamp(counter / reloadTime, 0f, 1f);

            laserReloadProgressBar.fillAmount = normalizedCounter;
            laserReloadProgressBar.color = new Color(laserReloadProgressBar.color.r, laserReloadProgressBar.color.g, laserReloadProgressBar.color.b, normalizedCounter);
            laserReloadProgresText.text = Mathf.Ceil(normalizedCounter*100).ToString() + "%";

            //Debug.Log("Counter: " + counter);
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("Reloaded");
        laserReloaded = true;
    }

    public void TurnOffMeshcollider()
    {
        meshCollider.enabled = false;
    }

    public void TurnOnMeshcollider()
    {
        meshCollider.enabled = true;
    }
}
