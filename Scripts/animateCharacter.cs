using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateCharacter : MonoBehaviour
{
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(Time.time) * 0.4f, 0.0f);
    }
}
