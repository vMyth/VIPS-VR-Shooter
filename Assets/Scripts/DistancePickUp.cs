using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DistancePickUp : MonoBehaviour
{
    GameObject player;
    public bool isHealthPickUp = false;
    public bool isAmmoPickUp = false;

    public static bool activePickUp = false;

    public GameObject helperText;

    private void Awake()
    {
        helperText = GameObject.Find("PickUpHelperText");
        helperText.SetActive(false);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 3f)
        {
            activePickUp = true;
            helperText.gameObject.SetActive(true);
            PickUp();
        }
        else
        {
            activePickUp = false;
            helperText.gameObject.SetActive(false);
        }
    }

    private void PickUp()
    {
        if (Shoot.canPickUp)
        {
            if (isHealthPickUp)
            {
                player.GetComponent<PlayerHealth>().AddHealth(GetComponent<HealthPickUp>().healthAmount);
                Done();
            }
            else if (isAmmoPickUp)
            {
                player.GetComponent<Shoot>().AddAmmo(GetComponent<AmmoPickUp>().ammoAmount);
                Done();
            }
        }
    }
    void Done()
    {
        helperText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}