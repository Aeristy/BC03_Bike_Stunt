using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGarage : PopupBase
{
    public GameObject MainPanel;
    public GameObject GaragePanel;
    public GARAGE_STATE state = GARAGE_STATE.MAIN;

    protected override void OnEnable()
    {
        base.OnEnable();
        MainPanel.SetActive(true);
        GaragePanel.SetActive(false);
    }
    public void OnRace()
    {
        Hide();
        GameManager.Instance.LoadTestScene();
    }
    public void OnGarage()
    {
        MainPanel.SetActive(false);
        GaragePanel.SetActive(true);
        state = GARAGE_STATE.GARAGE;
    }
    public void OnBack()
    {
        if(state != GARAGE_STATE.MAIN)
        {
            state = GARAGE_STATE.MAIN;
            MainPanel.SetActive(true);
            GaragePanel.SetActive(false);
        }
            
    }
}

public enum GARAGE_STATE
{
    MAIN = 0,
    GARAGE = 1,
    CUSTOMIZE = 2,
    
}