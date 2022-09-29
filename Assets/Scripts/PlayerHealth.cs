using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    public bool isDead;

    //HealthBar 
    ////SliderImplementation
    //public Slider healthBar;
    

    //RadialImplementation
    public Image healthRadialFill;

    public TextMeshProUGUI healthText;

    public static PlayerHealth singleton;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        // Slider Implementation
        //healthBar.maxValue = maxHealth;
        //healthBar.value = currentHealth;
        //healthText.text = healthBar.value.ToString();

        healthRadialFill.fillAmount = currentHealth / 100;
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            if (!isDead) Dead();
        }
    }

    public void DamagePlayer(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("PLAYER DEAD");
        Destroy(gameObject.GetComponent<Shoot>());
    }
}
