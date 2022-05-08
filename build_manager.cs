using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class build_manager : MonoBehaviour
{
    public static build_manager instance;
    public int money;
    public Text moneydisplay;
    public GameObject turret;
    public static int moneyspent;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        moneydisplay.text = "$" + money;
    }

    public void purchaseturret(int cost)
    {
        moneyspent += cost;
        money -= cost;
        moneydisplay.text = "$" + money;
    }

    public void addmoney(int cash)
    {
        money += cash;
        moneydisplay.text = "$" + money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
