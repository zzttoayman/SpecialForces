using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class dummyHealth : MonoBehaviour
{
    public Slider slider;
    public float dummyMaxHealth = 10000f;
    public float dummyCurrentHealth = 0f;
    public float timeSuffering; 
    public Animator goblinAnim;
    public AudioSource deathSound;
    public NavMeshAgent goblinAgent;

    // Start is called before the first frame update
    void Start()
    {
        goblinAgent = GetComponent<NavMeshAgent>();
        SetMaxHealth(dummyMaxHealth);
        dummyCurrentHealth = dummyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {   
        if(dummyCurrentHealth <= 295f)
        {
            dummyCurrentHealth = 0f;
            Die();
        }
        slider.value = dummyCurrentHealth;
    }

    public void Die()
    {
        goblinAnim.SetBool("isDead", true);
        if(goblinAnim.GetBool("isDead"))
        {
            Canvas goblinCanvas = gameObject.GetComponentInChildren<Canvas>();
            Destroy(goblinCanvas);
        }
        goblinAgent.isStopped = true;
        Destroy(this.gameObject, 8f);
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
