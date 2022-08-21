using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    //public int health;
    Stats statScript;

    // Start is called before the first frame update
    void Start()
    {
        //health = 50;
        statScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerSlider2D = GetComponent<Slider>();
        playerSlider2D.maxValue = statScript.maxHealth;
        playerSlider3D.maxValue = statScript.maxHealth;

        statScript.health = statScript.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = statScript.health;
        playerSlider3D.value = playerSlider2D.value;
    }
}
