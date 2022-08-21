using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities1 : MonoBehaviour

{
    public Image[] abilityImage = new Image[4];
    public float cooldown = 5;
    bool[] isCooldown = new bool[4];
    public KeyCode[] ability = new KeyCode[4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            abilityImage[i].fillAmount = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKey(ability[i]) && !isCooldown[i])
            {
                isCooldown[i] = true;
                abilityImage[i].fillAmount = 1;
            }

            if (isCooldown[i])
            {
                abilityImage[i].fillAmount -= 1 / cooldown * Time.deltaTime;

                if (abilityImage[i].fillAmount <= 0)
                {
                    abilityImage[i].fillAmount = 0;
                    isCooldown[i] = false;
                }
            }
        }
        
    }

}