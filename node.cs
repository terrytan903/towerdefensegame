using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class node : MonoBehaviour
{
    public Color hovercolor;
    Color startcolor;
    Renderer r;
    GameObject turret;
    public GameObject upgradepanel;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        startcolor = r.material.color;
        upgradepanel.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        r.material.color = hovercolor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (build_manager.instance.turret == null)
        {
            return;
        }
        if (turret != null)
        {
            StartCoroutine(popoutmenu());
            return;
        }
        int cost = (build_manager.instance.turret.GetComponentInChildren<turret_ai>().cost);
        if (build_manager.instance.money >= cost)
        {
            turret = Instantiate(build_manager.instance.turret, transform.position + Vector3.up * build_manager.instance.turret.GetComponentInChildren<turret_ai>().offset, Quaternion.identity);
            build_manager.instance.purchaseturret(cost);
        }
    }

    IEnumerator popoutmenu()
    {
        yield return null;
        int tcost = turret.GetComponent<turret_ai>().cost;
        upgradepanel.SetActive(true);
        upgradepanel.transform.position = Input.mousePosition;
        upgradepanel.transform.GetChild(1).GetComponent<upgradepanel>().selectedturret = turret;
        Text t = upgradepanel.transform.GetChild(0).GetComponentInChildren<Text>();
        t.text = "Level " + turret.GetComponent<turret_ai>().level;
        Text[] ta = upgradepanel.transform.GetChild(1).GetComponentsInChildren<Text>();
        ta[0].text = "Upgrade" + " $" + (tcost + tcost * turret.GetComponent<turret_ai>().level);
        ta[1].text = "Sell" + " $" + 0.75 * tcost;
        ta[2].text = "Cancel";
        Debug.Log("turret already exists");
    }
    private void OnMouseExit()
    {
        r.material.color = startcolor;
    }
}
