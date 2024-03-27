using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBulletCollision : MonoBehaviour
{
    private BoxCollider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = gameObject.GetComponentsInChildren<BoxCollider>();
        Debug.Log(colliders);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
