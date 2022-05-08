using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopscript : MonoBehaviour
{
    public GameObject turret1;
    public GameObject missilelauncher;
    public GameObject laser;
    public GameObject aoe;
    public void selectturret1()
    {
        build_manager.instance.turret = turret1;
        Debug.Log("turretg 1 sleleicted");
    }
    public void selectmissilelauncher()
    {
        build_manager.instance.turret = missilelauncher;
        Debug.Log("msi lei lahncheer  sleleicted");
    }

    public void selectlaser()
    {
        build_manager.instance.turret = laser;
        Debug.Log("laser selsected");
    }

    public void selectaoe()
    {
        build_manager.instance.turret = aoe;
    }
}
