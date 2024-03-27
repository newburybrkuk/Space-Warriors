using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTextSetter : MonoBehaviour
{
    public int currentWeapon;
    public TMP_Text AmmoText;

    // Start is called before the first frame update
    void Start()
    {
        if (currentWeapon == 1)
        {
            AmmoText.text = PlayerPrefs.GetInt("PistolMagazineNumber").ToString() + "/" + PlayerPrefs.GetInt("PistolTotalAmmo").ToString();
        }

        if (currentWeapon == 2)
        {
            AmmoText.text = PlayerPrefs.GetInt("MiniMagazineNumber").ToString() + "/" + PlayerPrefs.GetInt("MiniTotalAmmo").ToString();
        }

        if (currentWeapon == 3)
        {
            AmmoText.text = PlayerPrefs.GetInt("SniperMagazineNumber").ToString() + "/" + PlayerPrefs.GetInt("SniperTotalAmmo").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
