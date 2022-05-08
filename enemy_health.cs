using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public int value;
    Material m;
    Color c;
    public static int enemieskilled;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<Renderer>().material;
        c = m.color;
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damagedealt(int damage)
    {
        health -= damage;
        float t = (float)health / maxhealth;
        m.color = Color.Lerp(Color.white, c, t);
//        transform.localScale = 0.5f * (scale + t * scale);
        if (health <= 0)
        {
            enemieskilled++;
            Destroy(gameObject);
            build_manager.instance.addmoney(value);
        }
    }
}
