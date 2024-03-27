using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;

public class Pickup : MonoBehaviour
{
    public GameObject holster;
    public HealthSystemComponent healthSystemComponent;
    private HealthSystem healthSystem;

    public AudioSource sound;
    public AudioClip pickupSound;

    public bool isUnlimited = false;

    void Start()
    {
        healthSystem = healthSystemComponent.GetHealthSystem();
    }
    
    void AmmoPickup()
    {
        Transform gunsParent = holster.transform;
        for (int i = 0; i < gunsParent.childCount; i++)
        {
            Gun gun = gunsParent.GetChild(i).gameObject.GetComponent<Gun>();
            int weight = Random.Range(0, 9);
            int scalar = 1;
            if (weight <= 5)
            {
                // do nothing
            } 
            else if (weight <= 8)
            {
                scalar = 2;
            }
            else if (weight <= 9)
            {
                scalar = 3;
            }
            gun.AddAmmo(scalar);
        }
        holster.GetComponent<WeaponSwitching>().UpdateAllAmmo();
    }

    void HealthPickup()
    {
        healthSystem.Heal(Random.Range(5, 20));
    }

    void powerUpAddAmmo()
    {
        Transform gunsParent = holster.transform;
        for (int i = 0; i < gunsParent.childCount; i++)
        {
            Gun gun = gunsParent.GetChild(i).gameObject.GetComponent<Gun>();
            gun.maxAmmo++;
        }
    }

    void powerupPickup()
    {
        isUnlimited = true;
        Invoke("powerupOff", 7f);
    }

    void powerupOff()
    {
        isUnlimited = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "ammo":
                Destroy(other.gameObject);
                AmmoPickup();
                sound.PlayOneShot(pickupSound, 0.5f);
                break;
            case "health":
                Destroy(other.gameObject);
                HealthPickup();
                sound.PlayOneShot(pickupSound, 0.5f);
                break;
            case "powerup":
                Destroy(other.gameObject);
                powerUpAddAmmo();
                powerupPickup();
                sound.PlayOneShot(pickupSound, 0.5f);
                break;
        }
    }
}
