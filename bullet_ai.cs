using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_ai : MonoBehaviour
{
    Transform target;
    public float speed;
    public int damage;
    public GameObject collisioneffect;
    public float explosionradius;
    public float hitdetectiondistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (explosionradius > 0)
            {
                getnewtarget();
            }
            else
            {
                Destroy(gameObject);
            }
            return;
        }
        tracktarget();
        rotatetowards();
    }

    public void gettarget(Transform t)
    {
        target = t;
    }

    void rotatetowards()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
    void tracktarget()
    {
        Vector3 dir = target.position - transform.position;
        float distancethisframe = speed * Time.deltaTime;
        if (dir.magnitude < hitdetectiondistance)
        {
            if (explosionradius > 0)
            {
                explosion();
            }
            else
            {
                hittarget();
            }
            return;
        }
        transform.position += dir.normalized * distancethisframe;
    }

    void hittarget()
    {
        GameObject go = Instantiate(collisioneffect, transform.position, Quaternion.identity);
        Destroy(go, 3);
        target.GetComponent<enemy_health>().damagedealt(damage);
        Destroy(gameObject);
    }

    void explosion()
    {
        GameObject go = Instantiate(collisioneffect, transform.position, Quaternion.identity);
        Destroy(go, 3);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionradius);
        foreach (Collider c in colliders)
        {
            if (c.tag == "enemy")
            {
                c.GetComponent<enemy_health>().damagedealt(damage);
            }
        }
        Destroy(gameObject);
    }

    void getnewtarget()
    {
        target = null;
        float shortest = 1000;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            float current = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortest > current)
            {
                shortest = current;
                target = enemy.transform;
            }
        }
        if (target == null)
        {
            explosion();
        }
    }
}
