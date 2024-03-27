using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ammoSwitch : MonoBehaviour
{
    [Header("Ammo Counters")]
    public RectTransform pistol;
    public RectTransform sniper;
    public RectTransform minigun;
    
    private int activeIndex = 0;
    private RectTransform[] ammoCounts;

    void setActive(RectTransform counter)
    {
        counter.localScale = new Vector3(1, 1, 1);
        counter.localPosition = new Vector3(-240, -190, 0);
    }

    void setInActive(RectTransform counter)
    {
        counter.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        counter.localPosition = new Vector3(-290, -127, 0);
    }

    void inactiveCycle(RectTransform counter)
    {
        counter.localPosition = new Vector3(-290, -152, 0);
    }

    int incrementIndex(int index, int direction)
    {
        index += direction;
        if (index > 2)
        {
            index = 0;
        } 
        else if (index < 0)
        {
            index = 2;
        }

        return index;
    }

    public void cycle(int direction)
    {
        setInActive(ammoCounts[activeIndex]);
        activeIndex = incrementIndex(activeIndex, direction);
        setActive(ammoCounts[activeIndex]);
        inactiveCycle(ammoCounts[incrementIndex(activeIndex, direction)]);
    }

    // Start is called before the first frame update
    void Start()
    {
        ammoCounts = new RectTransform[3] { pistol, sniper, minigun };
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            cycle(1);
        } 
        else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            cycle(-1);
        }*/
    }
}
