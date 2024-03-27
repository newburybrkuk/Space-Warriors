using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextTutorial : MonoBehaviour
{
    public GameObject Tutorial2;
    public GameObject Tutorial3;
    public GameObject Tutorial4;
    public GameObject Tutorial5;

    Vector3 Tutorial2Position;
    Vector3 Tutorial3Position;
    Vector3 Tutorial4Position;
    Vector3 Tutorial5Position;

    public int currentTutorial;

    public AudioClip teleporterExit;
    public AudioSource sound;

    public Gun pistol;
    public Gun mini;
    public Gun sniper;

    void Start()
    {
        Tutorial2Position = Tutorial2.transform.position;
        Tutorial3Position = Tutorial3.transform.position;
        Tutorial4Position = Tutorial4.transform.position;
        Tutorial5Position = Tutorial5.transform.position;
    }

    public void Teleport(GameObject other)
    {
        if (currentTutorial == 1)
        {
            other.transform.position = Tutorial2Position;
        }

        if (currentTutorial == 2)
        {
            other.transform.position = Tutorial3Position;
        }

        if (currentTutorial == 3)
        {
            other.transform.position = Tutorial4Position;
        }

        if (currentTutorial == 4)
        {
            other.transform.position = Tutorial5Position;
        }

        if (currentTutorial == 5)
        {

            SceneManager.LoadScene(3);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Teleport(other.gameObject);
            sound.PlayOneShot(teleporterExit, 1f);
        }
    }
}
