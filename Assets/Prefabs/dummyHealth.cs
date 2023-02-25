using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dummyHealth : MonoBehaviour
{
    public Slider slider;
    public float dummyMaxHealth = 10000f;
    public float dummyCurrentHealth = 0f;
    public float timeSuffering; 
    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth(dummyMaxHealth);
        dummyCurrentHealth = dummyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {   
        if(dummyCurrentHealth < 0.05f)
        {
            Destroy(this.gameObject);
        }
        slider.value = dummyCurrentHealth;
    }

    public void TakeDamage(float damage)
    {
        dummyCurrentHealth -= damage;
        SetHealth(dummyCurrentHealth);
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
