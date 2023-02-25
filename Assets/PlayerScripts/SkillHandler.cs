using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.AI;
using UnityEngine.UI;

public class SkillHandler : MonoBehaviour
{

    #region FartSkill
    public GameObject fartParticles;
    public ParticleSystem fartParticleSystem;
    GameObject fartChild;
    Transform playerTransform;
    NavMeshAgent playerAgent;
    public PlayerMovement movement;

    bool hasSpawned = false;
    public float cooldownTime = 4f;
    private bool onCooldown = false;

    public PlayerHealth playerHealth;
    float dmgPerSec = 2f;

    float fartRadius = 11f;
    float fartRange = 6f;

    public dummyHealth dummy;

    public Slider skillCooldownOverlaySlider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            fartSkill();
        }
        fartSkillUpdate();
    }

    public void FartButtonClicked()
    {
        fartSkill();
    }

    void fartSkill()
    {
        if (!onCooldown)
        {
            if (fartChild)
            {
                onCooldown = true;
                SetMaxFillOverlayAmount(cooldownTime);
                dummy.timeSuffering = 0f;
                playerAgent.speed = movement.defSpeed;
                fartParticleSystem = fartChild.GetComponent<ParticleSystem>();
                fartParticleSystem.Stop();
                Destroy(fartChild, 3f);
                hasSpawned = false;
                SetOverlayAmount(cooldownTime);
                SetMaxFillOverlayAmount(cooldownTime);
                Invoke("ResetCooldown", cooldownTime);
            }
            else
            {
                fartChild = Instantiate(fartParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                hasSpawned = true;
                playerAgent.speed = movement.defSpeed / 3f;
                onCooldown = false;
            }
        }
    }

    void fartSkillUpdate()
    {
        UpdateOverlayAmount(1f);
        if(skillCooldownOverlaySlider.value < 0.05f)
        {
            onCooldown = false;
            SetOverlayAmount(0);
        }
        if (hasSpawned) 
        {
            playerHealth.currentHealth -= dmgPerSec * Time.deltaTime;
            playerHealth.SetHealth(playerHealth.currentHealth);
            fartChild.transform.position = playerTransform.position;
            Collider[] colliders = Physics.OverlapSphere(transform.position, fartRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    Vector3 direction = col.transform.position - transform.position;
                    float distance = direction.magnitude;
                    if (distance < fartRange)
                    {
                        dummy = col.gameObject.GetComponent<dummyHealth>();
                        Debug.Log("Enemy hit!");
                        dummy.timeSuffering += Time.deltaTime;
                        dummy.dummyCurrentHealth -= Mathf.Pow(2f,(1f + dummy.timeSuffering));
                        dummy.slider.value = dummy.dummyCurrentHealth;
                    } else 
                    {
                        dummy.timeSuffering = 0f;
                    }
                }
            }
        }
    }

    void ResetCooldown()
    {
        onCooldown = false;
    }

    public void SetMaxFillOverlayAmount(float cooldownTime)
    {
        skillCooldownOverlaySlider.maxValue = cooldownTime;
    }

    public void SetOverlayAmount(float OverlayAmount)
    {
        skillCooldownOverlaySlider.value = OverlayAmount;
    }

    void UpdateOverlayAmount(float updateAmount)
    {
        skillCooldownOverlaySlider.value -= updateAmount * Time.deltaTime;
    }
}