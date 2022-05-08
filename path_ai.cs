using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path_ai : MonoBehaviour
{
    Transform[] Waypoints;
    int waypoint;
    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Waypoints = GameObject.Find("Waypoints").transform.GetComponentsInChildren<Transform>();
        waypoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoint < Waypoints.Length)
        {
        Vector3 dir = Waypoints[waypoint].transform.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
            if (dir.magnitude < 0.1f)
            {
                transform.position = Waypoints[waypoint].transform.position;
                waypoint += 1;
            }
        }
        if (waypoint >= Waypoints.Length)
        {
            reached_end();
        }
    }

    void reached_end()
    {
        base_health.health -= damage;
        Destroy(gameObject);
    }
}
