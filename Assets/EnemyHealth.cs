using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider enemySlider3D;

    //public int health;
    Stats statScript;

    // Start is called before the first frame update
    void Start()
    {
        //health = 50;
        statScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Stats>();


        enemySlider3D.maxValue = statScript.maxHealth;

        statScript.health = statScript.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {

        enemySlider3D.value = statScript.health;
    }
}
