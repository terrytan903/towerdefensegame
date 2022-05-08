using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class base_health : MonoBehaviour
{
    public static int health;
    public Slider healthbar;
    public GameObject endscreen;
    public Text stats;
    // Start is called before the first frame update
    void Start()
    {
        build_manager.moneyspent = 0;
        enemy_health.enemieskilled = 0;
        enemy_spawner.wave = 0;
        Time.timeScale = 1;
        endscreen.SetActive(false);
        health = 10;
        healthbar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health;
        if (health <= 0)
        {
            gameover();
        }
    }

    void gameover()
    {
        Time.timeScale = 0;
        endscreen.SetActive(true);
        stats.text = "Final wave: " + enemy_spawner.wave + "\n" +
                     "Enemies destroyed: " + enemy_health.enemieskilled + "\n" +
                     "Money spent: " + build_manager.moneyspent;
    }
}
