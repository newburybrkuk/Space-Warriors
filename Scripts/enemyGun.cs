using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.HealthSystemCM;


public class enemyGun : MonoBehaviour
{
    public bool canFire;

    [Header("Raycast Origin")]
    public Transform gun;

    [Header("Gun Stats")]
    public float range = 20;
    public float impactForce = 150;
    [Range(0, 100)]
    public int damage;
    [Range(0, 0.2f)]
    public float inaccuracy;

    public int fireRate = 2;
    private float nextTimeToFire = 0;

    [Header("Particle System")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public WFX_LightFlicker lightFlash;

    [Header("Ammo")]
    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineSize = 30;

    [Header("Reload")]
    public float reloadTime = 2f;
    public bool isReloading;

    [Header("Animator")]
    public Animator animator;

    public AudioSource sound;
    public AudioClip shootingSound;


    InputAction shoot;

    public Pickup powerup;

    // Start is called before the first frame update
    void Start()
    {  
        currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("reloading", false);
    }



    // Update is called once per frame
    void Update()
    {
        if (!isReloading && canFire)
        {

            if (currentAmmo == 0 && magazineSize == 0)
            {
                animator.SetBool("reloading", true);
                return;
            }

            if (currentAmmo == 0 && magazineSize > 0)
            {
                StartCoroutine(Reload());
            }
            else if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Fire();
                sound.PlayOneShot(shootingSound, 0.5f);

            }

        }
    }

    private void Fire()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        lightFlash.FlickerOnce();
        currentAmmo--;

        Vector3 start = gun.parent.position + new Vector3(0, 1.7f, 0);

        float xOffset = Random.Range(-inaccuracy, inaccuracy);
        float yOffset = Random.Range(-inaccuracy, inaccuracy);
        float zOffset = Random.Range(-inaccuracy, inaccuracy);

        Vector3 hitOffset = new Vector3(xOffset, yOffset, zOffset);
        Vector3 hitPosition = gun.forward + hitOffset;
        if (Physics.Raycast(start, hitPosition, out hit, range))
        {   
            /*if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }*/

            if (hit.collider != null)
            {
                // getting the transform from the collider hit
                Transform hitTransform = hit.collider.gameObject.transform;
                // check if the player was hit and find its root (contains the health system)
                if (hitTransform.CompareTag("Player"))
                {

                float damageInstance;
                
                damageInstance = damage;


                HealthSystem playerHealth = hitTransform.gameObject.GetComponent<HealthSystemComponent>().GetHealthSystem();

                playerHealth.Damage(damageInstance);
                }
                else
                {
                    Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                    GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
                    impact.transform.parent = hit.transform;
                    Destroy(impact, 5);
                }
            }
        }

    }
/*
    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }*/

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("reloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("reloading", false);
        if (magazineSize >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineSize -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineSize;
            magazineSize = 0;
        }
        isReloading = false;
        nextTimeToFire = Time.time + 1f / fireRate;
    }


}

