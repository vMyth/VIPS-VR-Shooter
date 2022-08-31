using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField]
    int currentAmmo;

    [SerializeField]
    Transform shootPoint;

    [SerializeField] 
    float rof;
    float nextFire = 0;

    [SerializeField]
    float weaponRange;

    //For Deducting Enemy Heakth with Each Shot
    [SerializeField] 
    float damageEnemy = 10f;

    //Weapon Effects
    //MuzzleFlash
    public ParticleSystem muzzleFlash;

    private void Start()
    {
        muzzleFlash.Stop();
    }

    void OnShoot()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + rof;

            currentAmmo--;

            if(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
            {
                //Debug.DrawRay(shootPoint.position, shootPoint.forward, Color.red, 0.1f);

                if(hit.transform.tag == "Enemy")
                {
                    Debug.Log("ENEMY SHOT");
                    EnemyHealth EnemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                    EnemyHealthScript.DeductHealth(damageEnemy);
                }
                else
                {
                    Debug.Log("SOMETHING ELSE SHOT");
                }
            }
        }
    }
}
