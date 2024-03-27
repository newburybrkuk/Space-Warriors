using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
    public bool isScoped = false;
    //InputAction scope;
    public Animator animator;
    public GameObject scopeOverlay;
    public Camera fpsCam;
    
    // Start is called before the first frame update
    void Start()
    {
       /* scope = new InputAction("Scope", binding: "<mouse>/rightButton", interactions: "hold(duration=0.2)");

        scope.Enable();*/
    }

    // Update is called once per frame
    void Update()
    {
        Gun gun = FindObjectOfType<Gun>();
        if (gun.isReloading || gun.currentAmmo == 0) 
        { 
            OnUnscoped();
        }
        else
        {
            if (Input.GetButton("Fire2"))
            {
                isScoped = true;
                StartCoroutine(OnScoped());
            }
            else
            {
                isScoped = false;
                OnUnscoped();
            }
            
        }
        
    }
    IEnumerator OnScoped()
    {
        animator.SetBool("isScoped", true);
        yield return new WaitForSeconds(0.25f);
        fpsCam.fieldOfView = 20;
        scopeOverlay.SetActive(true);
        fpsCam.cullingMask = fpsCam.cullingMask & ~(1 << 11);
    }

    void OnUnscoped()
    {
        animator.SetBool("isScoped", false);
        fpsCam.fieldOfView = 60;
        scopeOverlay.SetActive(false);
        fpsCam.cullingMask = fpsCam.cullingMask | (1 << 11);
    }

}
