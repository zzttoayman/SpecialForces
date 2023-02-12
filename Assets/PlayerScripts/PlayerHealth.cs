using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player; 
    public float maxHealth = 100f;
    public float currentHealth;

    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(currentHealth < 30f)
        {
            Destroy(player);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health; 
    }
}
