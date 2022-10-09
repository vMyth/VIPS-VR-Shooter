using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //For Deducting Enemy Health with Each Shot
    [SerializeField] 
    float damageEnemy = 10f;

    [SerializeField]
    float headShotDamage = 100f;


    //Weapon Effects
    //MuzzleFlash
    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEject;

    //Audio
    AudioSource gunAudioSource;
    public AudioClip fireAudioClip;
    public AudioClip dryFireAudioClip;
    public AudioClip headShotAudioClip;

    //Bullet Impact Effect
    public GameObject bloodEffect;
    public GameObject wallEffect;

    //Shooting Animations
    public Animator anim;

    //Display Ammo on Screen
    public TextMeshProUGUI ammoText;

    //Helping in PickUp
    public static bool canPickUp = false;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        muzzleFlash.Stop();
        cartridgeEject.Stop();
        gunAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        ammoText.text = currentAmmo.ToString() + " / " + carriedAmmo.ToString();

        PlayerPrefs.SetInt("Ammo", carriedAmmo + currentAmmo);
        PlayerPrefs.SetFloat("Health", PlayerHealth.singleton.currentHealth);

        if (EnemySpawner.enemiesActive)
        {
           Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                GameOver();
            }
        }
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

    void OnPick()
    {
        if (DistancePickUp.activePickUp)
        {
            canPickUp = true;
        }
        else
        {
            canPickUp = false;
        }
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
            else if (hit.transform.tag == "Head")
            {
                Debug.Log("HeadShot");
                EnemyHealth EnemyHealthScript = hit.transform.GetComponentInParent<EnemyHealth>();
                EnemyHealthScript.DeductHealth(headShotDamage);
                if (!hit.transform.GetComponentInParent<EnemyHealth>().isEnemyDead) Instantiate(bloodEffect, hit.point, Quaternion.identity);
                hit.transform.gameObject.SetActive(false);
                gunAudioSource.PlayOneShot(headShotAudioClip);
            }
            else
            {
                Debug.Log(hit.transform.name);
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
        if (timer <= 0)
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

    public void AddAmmo(int addOnAmmo)
    {
        carriedAmmo += addOnAmmo;
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
