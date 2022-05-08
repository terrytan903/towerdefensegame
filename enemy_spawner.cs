using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_spawner : MonoBehaviour
{
    public GameObject[] capsule_enemies;
    public float spawn_interval;
    public int enemycount;
    public int enemyhealth;
    public float enemyspeed;
    public int healthgainedperwave;
    public float timebetweenwaves;
    public static int wave = 0;
    bool waverunning;
    float timer;
    public Text countdown;
    public Text wavedisplay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnenemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnwave()
    {
        int enemiesspawned = 0;
        waverunning = true;
        wave++;
        wavedisplay.text = "Wave: " + wave;
        while (enemiesspawned < enemycount)
        {
            spawnenemy();
            yield return new WaitForSeconds(spawn_interval);
            enemiesspawned++;
        }

        while (waverunning == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            if (enemies.Length == 0)
            {
                waverunning = false;
                timer = 0;
            }
            yield return new WaitForSeconds(spawn_interval);
        }
        enemycount += 1 * wave;
    }

    IEnumerator spawnenemies()
    {
        while (true)
        {
            if (!waverunning)
            {
                countdown.text = (Mathf.CeilToInt(timebetweenwaves - timer)).ToString();
                if (timer > timebetweenwaves)
                {
                    StartCoroutine(spawnwave());
                    countdown.text = "0";
                }
                timer += 0.1f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void spawnenemy()
    {
        GameObject go = Instantiate(capsule_enemies[Random.Range(0,capsule_enemies.Length)], transform.position, Quaternion.identity);
        go.GetComponent<enemy_health>().maxhealth += (int)(0.1f * go.GetComponent<enemy_health>().maxhealth * wave * (1 + startscreencontroller.difficulty));
        // go.GetComponent<path_ai>().speed += enemyspeed;
    }
}
