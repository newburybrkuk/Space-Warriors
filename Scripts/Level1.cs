using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    public int CurrentLevel;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(5);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (CurrentLevel == 1)
        {
            SceneManager.LoadScene(4);
        }
        if (CurrentLevel == 2)
        {
            SceneManager.LoadScene(5);
        }
        if (CurrentLevel == 3)
        {
            SceneManager.LoadScene(6);
        }
    }
}
