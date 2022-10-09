using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPickUp : MonoBehaviour
{
    GameObject player;
    public bool isHealthPickUp = false;
    public bool isAmmoPickUp = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
        Destroy(gameObject);
    }
}
