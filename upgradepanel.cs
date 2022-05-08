using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradepanel : MonoBehaviour
{
    public GameObject selectedturret;
    public float refundpercent;
    public void upgrade()
    {
        int cost = selectedturret.GetComponent<turret_ai>().cost + selectedturret.GetComponent<turret_ai>().cost * selectedturret.GetComponent<turret_ai>().level;
        if(selectedturret != null)
        {
            if (cost < build_manager.instance.money)
            {
                selectedturret.GetComponent<turret_ai>().level += 1;
                build_manager.instance.purchaseturret(cost);
            }
        }
        selectedturret = null;
        transform.parent.gameObject.SetActive(false);
    }
    public void sell()
    {
        if (selectedturret != null)
        {
            int cost = selectedturret.GetComponent<turret_ai>().cost;
            build_manager.instance.addmoney((int)(refundpercent * cost));
            Destroy(selectedturret);
        }
        transform.parent.gameObject.SetActive(false);
    }
    public void cancel()
    {
        selectedturret = null;
        transform.parent.gameObject.SetActive(false);
    }
}
