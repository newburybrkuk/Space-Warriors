using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setAmmo();
    }

    void setAmmo()
    {
        PlayerPrefs.SetInt("PistolMagazineNumber", 12);
        PlayerPrefs.SetInt("PistolTotalAmmo", 36);

        PlayerPrefs.SetInt("MiniMagazineNumber", 30);
        PlayerPrefs.SetInt("MiniTotalAmmo", 60);

        PlayerPrefs.SetInt("SniperMagazineNumber", 2);
        PlayerPrefs.SetInt("SniperTotalAmmo", 2);
    }
}
