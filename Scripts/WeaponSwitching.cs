using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public int selectedWeapon = 0;
    public ammoSwitch ammoUI;
    Scope scope;
    public TextMeshProUGUI pistolAmmo;
    public TextMeshProUGUI sniperAmmo;
    public TextMeshProUGUI minigunAmmo;
    private TextMeshProUGUI ammoInfoText;
    private TextMeshProUGUI[] ammoArray;

    // Start is called before the first frame update
    void Start()
    {
        switching = new InputAction("Scroll", binding: "<Mouse>/scroll");
        switching.AddBinding("<Gamepad>/Dpad");
        switching.Enable();

        ammoArray = new TextMeshProUGUI[3] { pistolAmmo, minigunAmmo, sniperAmmo };

        UpdateAllAmmo();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        ammoInfoText = ammoArray[selectedWeapon];
        Gun currentGun = FindObjectOfType<Gun>();
        ammoInfoText.text = currentGun.currentAmmo + " / " + currentGun.maxAmmo;

        float scrollValue = switching.ReadValue<Vector2>().y;

        int previousSelected = selectedWeapon;
        if (!Input.GetButton("Fire2"))
        {
            if (scrollValue > 0)
            {
                selectedWeapon++;
                ammoUI.cycle(-1);
                if (selectedWeapon == 3)
                    selectedWeapon = 0;
            }
            else if (scrollValue < 0)
            {
                selectedWeapon--;
                ammoUI.cycle(1);
                if (selectedWeapon == -1)
                    selectedWeapon = 2;
            }
        }
        
        if(previousSelected != selectedWeapon)
        {
            SelectWeapon();
        }
        
    }

    public void UpdateAllAmmo()
    {
        for (int i = 0; i < 3; i++)
        {
            Gun gun = transform.GetChild(i).gameObject.GetComponent<Gun>();
            ammoArray[i].text = gun.currentAmmo + " / " + gun.maxAmmo;
        }
    }

    private void SelectWeapon()
    { foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true); 
    }

}
