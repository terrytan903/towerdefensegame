using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_ai : MonoBehaviour
{
    public GameObject currenttarget;
    public GameObject bullet;
    public float attackrate;
    float timer = 0;
    public GameObject bulletposition;
    public float attackrange;
    public int cost;
    public enum turrettype {standard = 0, missile = 1, laser = 2, aoe = 3}
    public turrettype type;
    LineRenderer laser;
    public GameObject pivot;
    enemy_health targethealth;
    public int laserdamage;
    public GameObject lasercollisioneffect;
    GameObject laser_collision_effect;
    public float offset;
    public int level = 0;
    List<GameObject> targets;
    GameObject[] lines;
    // Start is called before the first frame update
    void Start()
    {
        if (type == turrettype.laser)
        {
            laser_collision_effect = Instantiate(lasercollisioneffect);
            laser_collision_effect.GetComponent<ParticleSystem>().Stop();
            GameObject go = Instantiate(bullet);
            laser = go.GetComponent<LineRenderer>();
            laser.SetPosition(0, Vector3.zero);
            laser.SetPosition(1, Vector3.zero);
        }
        if (type == turrettype.aoe)
        {
            lines = new GameObject[100];
            for (int i = 0; i < 100; i++)
            {
                lines[i] = Instantiate(bullet);
                laser = lines[i].GetComponent<LineRenderer>();
                laser.SetPosition(0, Vector3.zero);
                laser.SetPosition(1, Vector3.zero);
                lines[i].transform.parent = transform;
            }
        }
        InvokeRepeating("getnewenemy", 0.5f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (currenttarget != null)
        {
            if (type == turrettype.laser)
            {
                followtarget();
                firelaser();
                if (timer > attackrate)
                {
                    applylaserdamage();
                    timer = 0;
                }
            }
            else if (type == turrettype.aoe)
            {
                firelasers();
                if (timer > attackrate)
                {
                    applyaoedamage();
                    timer = 0;
                }
            }
            else
            {
                followtarget();
                if (timer > attackrate)
                {
                    attack();
                    timer = 0;
                }
            }
        }
        else
        {
            if (type == turrettype.laser)
            {
                laser.SetPosition(0, Vector3.zero);
                laser.SetPosition(1, Vector3.zero);
                laser_collision_effect.GetComponent<ParticleSystem>().Stop();
            }
         }
        timer += Time.deltaTime;
    }

    void followtarget()
    {
        Vector3 direction = currenttarget.transform.position - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        pivot.transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    void getnewenemy()
    {
        targethealth = null;
        currenttarget = null;
        float shortest = 1000;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        targets = new List<GameObject>();
        if (type == turrettype.aoe)
        {
            for (int i = 0; i < 100; i++)
            {
                laseroff(i);
            }
        }
        foreach (GameObject enemy in enemies)
        {
            float current = Vector3.Distance(transform.position, enemy.transform.position);
            if (current < attackrange){
                if (shortest > current)
                {
                    shortest = current;
                    currenttarget = enemy;
                    targethealth = currenttarget.GetComponent<enemy_health>();
                }
                if (type == turrettype.aoe)
                {

                    targets.Add(enemy);
                }
            }
        }
        if (type == turrettype.laser)
        {
            if (currenttarget != null)
            {
                laser_collision_effect.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                laser_collision_effect.GetComponent<ParticleSystem>().Stop();
            }
        }
        if (type == turrettype.aoe)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                laseron(i);
            }
        }
    }

    void attack()
    {
        GameObject go = Instantiate(bullet, bulletposition.transform.position, Quaternion.identity);
        bullet_ai b = go.GetComponent<bullet_ai>();
        b.gettarget(currenttarget.transform);
        b.damage += (int)(level * 0.5f * b.damage);

    }

    void firelaser()
    {
        laser.SetPosition(0, bulletposition.transform.position);
        laser.SetPosition(1, currenttarget.transform.position);
        laser_collision_effect.transform.position = currenttarget.transform.position;
        laser_collision_effect.transform.LookAt(bulletposition.transform.position);
        laser_collision_effect.transform.position += laser_collision_effect.transform.forward;
    }

    void applylaserdamage()
    {
        if (targethealth != null)
        {
            targethealth.damagedealt(laserdamage + (int)(level * 0.5f * laserdamage));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackrange);
    }

    void applyaoedamage()
    {
        foreach (GameObject t in targets)
        {
            if (t != null) {
                t.GetComponent<enemy_health>().damagedealt(laserdamage + (int)(level * 0.5f * laserdamage));
            }
        }

    }

    void laseroff(int i)
    {
        laser = lines[i].GetComponent<LineRenderer>();
        laser.SetPosition(0, Vector3.zero);
        laser.SetPosition(1, Vector3.zero);
    }

    void laseron(int i)
    {
        laser = lines[i].GetComponent<LineRenderer>();
        laser.SetPosition(0, bulletposition.transform.position);
        laser.SetPosition(1, targets[i].transform.position);
    }

    void firelasers()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                laseroff(i);
            }
        }
    }
}
