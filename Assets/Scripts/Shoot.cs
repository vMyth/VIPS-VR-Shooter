using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;

    //Ammo & Reloading
    public int currentAmmo = 12;
    public int maxAmmo = 12;
    public int carriedAmmo = 60;
    public bool isReloading = false;

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
    public ParticleSystem cartridgeEject;

    //Audio
    AudioSource gunAudioSource;
    public AudioClip fireAudioClip;
    public AudioClip dryFireAudioClip;

    //Bullet Impact Effect
    public GameObject bloodEffect;
    public GameObject wallEffect;

    //Shooting Animations
    public Animator anim;

    private void Start()
    {
        muzzleFlash.Stop();
        cartridgeEject.Stop();
        gunAudioSource = GetComponent<AudioSource>();
    }

    void OnShoot()
    {
        if (!isReloading)
        {
            if(currentAmmo > 0)
            {
                Fire();
            }
            else
            {
                DryFire();
            }
            
        }
    }

    void OnReload()
    {
        if (currentAmmo == maxAmmo) return;
        Reload();
    }
    void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rof;

            StartCoroutine(WeaponEffects());
            gunAudioSource.PlayOneShot(fireAudioClip);
            anim.SetTrigger("Shoot");

            currentAmmo--;

            ShootRay();
        }
    }
    void DryFire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rof;

            gunAudioSource.PlayOneShot(dryFireAudioClip);
            anim.SetTrigger("Shoot");
        }
    }
    void ShootRay()
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange))
        {
            //Debug.DrawRay(shootPoint.position, shootPoint.forward, Color.red, 0.1f);

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("ENEMY SHOT");
                EnemyHealth EnemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                EnemyHealthScript.DeductHealth(damageEnemy);
                if (!hit.transform.GetComponent<EnemyHealth>().isEnemyDead) Instantiate(bloodEffect, hit.point, Quaternion.identity);
            }
            else
            {
                Debug.Log("SOMETHING ELSE SHOT");
                Instantiate(wallEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }
        }
    }
    void Reload()
    {
        if (!isReloading)
        {
            if (carriedAmmo <= 0) return;
            anim.SetTrigger("Reload");
            StartCoroutine(ReloadCountDown(2f));
        }
    }
    IEnumerator ReloadCountDown(float timer)
    {
        while(timer > 0)
        {
            isReloading = true;
            timer -= Time.deltaTime;
            yield return null;
        }
        if(timer <= 0)
        {
            isReloading = false;
            int bulletsNeededToFillMag = maxAmmo - currentAmmo;
            int bulletsToDeduct = (carriedAmmo >= bulletsNeededToFillMag) ? bulletsNeededToFillMag : carriedAmmo;

            carriedAmmo -= bulletsToDeduct;
            currentAmmo += bulletsToDeduct;
        }
    }
    IEnumerator WeaponEffects()
    {
        muzzleFlash.Play();
        cartridgeEject.Play();
        yield return new WaitForEndOfFrame();
        muzzleFlash.Stop();
        cartridgeEject.Stop();
    }
}
