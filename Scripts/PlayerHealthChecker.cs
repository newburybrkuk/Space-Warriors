using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;
using UnityEngine.SceneManagement;

public class PlayerHealthChecker : MonoBehaviour
{
    private HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        HealthSystemComponent healthSystemComponent = gameObject.GetComponent<HealthSystemComponent>();
        healthSystem = healthSystemComponent.GetHealthSystem();   
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.IsDead())
        {
            SceneManager.LoadScene("SciFi_Warehouse");
        }
    }
}
