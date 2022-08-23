using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;

    public int currentAmmo;

    public Transform shootPoint;

    public float rof;
    public float nextFire = 0;

    public float weaponRange;

    void OnShoot()
    {
        Debug.Log("Shoot");
    }
}
