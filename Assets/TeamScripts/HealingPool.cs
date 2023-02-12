using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealingPool : MonoBehaviour
{
    public GameObject particles;
    public ParticleSystem shineParticles;
    public float sphereCastRadius = 1f;
    public float sphereCastDistance = 1f;
    public PlayerHealth playerHealth;
    private bool playerColliding = false;

    void Start()
    {
        particles.SetActive(false);
        shineParticles.Stop();
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCastRadius, LayerMask.GetMask("Player"));
        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.tag == "Player")
                {
                    playerColliding = true;
                    if(playerHealth.currentHealth < playerHealth.maxHealth)
                    {
                        float currentHealth = playerHealth.currentHealth += 5f * Time.deltaTime;
                        playerHealth.SetHealth(currentHealth);
                        break;
                    }
                }
            }
        }
        else
        {
            playerColliding = false;
        }

        if (playerColliding)
        {
            particles.SetActive(true);
            shineParticles.Play();
        }
        else
        {
            shineParticles.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
}