using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    public GameObject credits;

    public void openCredits()
    {
        credits.SetActive(true);
    }

    public void closeCredits()
    {
        credits.SetActive(false);
    }
}
