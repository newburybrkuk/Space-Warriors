using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.HealthSystemCM;
using System.Collections.Generic;


public class Gun : MonoBehaviour
{
    public float ClipLength = 1f;
    public GameObject AudioClip;

    [Header("Raycast Origin")]
    public Transform fpsCam;

    [Header("Gun Stats")]
    public float range = 20;
    public float impactForce = 150;
    [Range(0, 100)]
    public int damage;
    [Range(1, 5)]
    public float headshotMultiplier;
    
    public int fireRate = 10;
    private float nextTimeToFire = 0;
    
    [Header("Particle System")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public WFX_LightFlicker lightFlash;

    [Header("Ammo")]
    public int currentAmmo = 10;
    public int magazineSize = 10;
    public int maxAmmo = 30;

    [Header("Reload")]
    public float reloadTime = 2f;
    public bool isReloading;

    [Header("Animator")]
    public Animator animator;

    InputAction shoot;

    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioSource sound;

    public int currentWeapon;

    public Pickup powerup;
    
    // Start is called before the first frame update
    void Start()
    {
        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();

        currentAmmo = magazineSize;

      
    }
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    public void AddAmmo(int scalar)
    {
        int amount = (scalar * magazineSize) / 3;
        maxAmmo += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmo ==0 && maxAmmo == 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;
        bool isShooting = shoot.ReadValue<float>() == 1;
        animator.SetBool("isShooting", isShooting);

        if ((currentAmmo == 0 || (Input.GetButtonDown("Reload")) && currentAmmo < magazineSize) && !isReloading && maxAmmo > 0)
        {
            StartCoroutine(Reload());
        }

        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            sound.PlayOneShot(shootSound, 0.5f);
            Fire();
        }

        
    }

    private void Fire()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        lightFlash.FlickerOnce();
        if (!powerup.isUnlimited)
        {
            currentAmmo--;
        }

        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);

            if (hit.collider != null)
            {
                // getting the transform from the collider hit
                Transform hitTransform = hit.collider.gameObject.transform;
                // check if an enemy was hit and find its root (contains the health system)
                if (hitTransform.CompareTag("HPcollider"))
                {
                    Transform parent = hitTransform.parent;
                    while (parent.CompareTag("EnemyRoot") == false)
                    {
                        parent = parent.parent;
                    }

                    // check if the hit was a headshot, and if so deal double damage
                    float damageInstance;
                    if (hitTransform.name == "Head")
                    {
                        damageInstance = damage * headshotMultiplier;
                    }
                    else
                    {
                        damageInstance = damage;
                    }

                    HealthSystem enemyHealth = parent.gameObject.GetComponent<HealthSystemComponent>().GetHealthSystem();

                    enemyHealth.Damage(damageInstance);

                 
                }
                
            }

        }

    }   
    
    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        sound.PlayOneShot(reloadSound, 0.5f);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("isReloading", false);
        
        int amountToReload;
        if (currentAmmo == 0)
        {
            amountToReload = magazineSize;
        } 
        else
        {
            amountToReload = magazineSize - currentAmmo;
        }

        if (maxAmmo <= amountToReload)
        {
            currentAmmo += maxAmmo;
            maxAmmo = 0;
        }
        else
        {
            currentAmmo += amountToReload;
            maxAmmo -= amountToReload;
        }


        isReloading = false;
    }
    
    
    
}

